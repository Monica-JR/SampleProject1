using Microsoft.EntityFrameworkCore;
using PlasticLicensingApp.Data;
using PlasticLicensingApp.Models;
using PlasticLicensingApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlasticLicensingApp.Services.Product
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ProductSubmissionViewModel?> GetProductSubmissionViewModelAsync(int supplierId)
        {
            var supplier = await _context.Suppliers.FirstOrDefaultAsync(s => s.ID == supplierId);
            if (supplier == null) return null;

            var parameters = await _context.Parameters.Where(p => p.IsActive).ToListAsync();

            return new ProductSubmissionViewModel
            {
                SupplierID = supplierId,
                Parameters = parameters.Select(p => new ParameterValueViewModel
                {
                    ParameterID = p.ID,
                    ParameterName = p.ParameterName
                }).ToList()
            };
        }

        public async Task<bool> SubmitProductAsync(ProductSubmissionViewModel model, int userId)
        {
            var status = await _context.Statuses
                .FirstOrDefaultAsync(s => s.StatusName == "Submitted" && s.StatusType == "Product")
                ?? await _context.Statuses.FirstOrDefaultAsync();

            if (status == null)
            {
                status = new Status
                {
                    StatusName = "Submitted",
                    StatusType = "Product",
                    IsActive = true,
                    CreatedBy = userId,
                    CreatedDate = DateTime.Now
                };
                _context.Statuses.Add(status);
                await _context.SaveChangesAsync();
            }

            var product = new Models.Product
            {
                ProductName = model.ProductName,
                SupplierID = model.SupplierID,
                StatusID = status.ID,
                CreatedBy = userId,
                CreatedDate = DateTime.Now
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            foreach (var param in model.Parameters)
            {
                var productParam = new ProductParameter
                {
                    ProductID = product.ID,
                    ParameterID = param.ParameterID,
                    ParameterValue = param.Value,
                    CreatedBy = userId,
                    CreatedDate = DateTime.Now
                };
                _context.ProductParameters.Add(productParam);
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Models.Product>> GetAllProductsAsync()
        {
            return await _context.Products
                .Include(p => p.Supplier)
                .Include(p => p.Status)
                .ToListAsync();
        }

        public async Task<Models.Product> CreateProductAsync(Models.Product product)
        {
            product.StatusID = 1; // Default to Pending
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }
    }
}
