using Microsoft.EntityFrameworkCore;
using PlasticLicensingApp.Data;
using PlasticLicensingApp.Models;
using PlasticLicensingApp.ViewModels;
using System;
using System.Threading.Tasks;

namespace PlasticLicensingApp.Services.Account
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext _context;

        public AccountService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User?> AuthenticateAsync(LoginViewModel model)
        {
            return await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == model.Email && u.PasswordHash == model.Password);
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<User> RegisterAsync(RegisterViewModel model)
        {
            var supplierRole = await _context.Roles.FirstOrDefaultAsync(r => r.RoleName == "Supplier");

            var address = new Address
            {
                Line1 = model.Line1,
                City = model.City,
                State = model.State,
                Pincode = model.Pincode,
                Country = model.Country,
                CreatedDate = DateTime.Now,
                CreatedBy = 1
            };

            _context.Addresses.Add(address);
            await _context.SaveChangesAsync();

            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PasswordHash = model.Password,
                RoleID = supplierRole?.ID ?? 2,
                AddressID = address.ID,
                CreatedDate = DateTime.Now,
                CreatedBy = 1
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }
    }
}
