using Microsoft.EntityFrameworkCore;
using PlasticLicensingApp.Data;
using PlasticLicensingApp.Models;
using PlasticLicensingApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlasticLicensingApp.Services.Supplier
{
    public class SupplierService : ISupplierService
    {
        private readonly ApplicationDbContext _context;

        public SupplierService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<SupplierDashboardViewModel>> GetSuppliersForUserAsync(int userId)
        {
            return await _context.Suppliers
                .Include(s => s.Address)
                .Where(s => s.CreatedBy == userId)
                .Select(s => new SupplierDashboardViewModel
                {
                    ID = s.ID,
                    CompanyName = s.SupplierCompanyName ?? string.Empty,
                    City = s.Address != null && s.Address.City != null ? s.Address.City : string.Empty,
                    State = s.Address != null && s.Address.State != null ? s.Address.State : string.Empty,
                    CreatedDate = s.CreatedDate
                }).ToListAsync();
        }

        public async Task<(bool Success, string? ErrorMessage)> RegisterSupplierCompanyAsync(SupplierRegistrationViewModel model, int userId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                int addressId;
                if (model.UseProfileAddress)
                {
                    var user = await _context.Users.FindAsync(userId);
                    if (user == null) return (false, "User not found");

                    if (user.AddressID == null)
                    {
                        return (false, "No profile address found. Please enter a new address.");
                    }
                    addressId = user.AddressID.Value;
                }
                else
                {
                    var address = new Address
                    {
                        Line1 = model.Line1 ?? "",
                        Line2 = model.Line2,
                        City = model.City ?? "",
                        State = model.State ?? "",
                        Pincode = model.Pincode ?? "",
                        Country = model.Country ?? "",
                        Phone = model.Phone ?? "",
                        CreatedBy = userId,
                        CreatedDate = DateTime.Now
                    };
                    _context.Addresses.Add(address);
                    await _context.SaveChangesAsync();
                    addressId = address.ID;
                }

                var supplier = new Models.Supplier
                {
                    SupplierCompanyName = model.SupplierCompanyName,
                    AddressID = addressId,
                    CreatedBy = userId,
                    CreatedDate = DateTime.Now
                };

                _context.Suppliers.Add(supplier);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return (true, null);
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return (false, "An error occurred while saving. Please try again.");
            }
        }

        public async Task<Address?> GetUserProfileAddressAsync(int userId)
        {
            var user = await _context.Users.Include(u => u.Address).FirstOrDefaultAsync(u => u.ID == userId);
            return user?.Address;
        }
    }
}
