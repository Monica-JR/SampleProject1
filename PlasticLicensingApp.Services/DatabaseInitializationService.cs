using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PlasticLicensingApp.Data;
using PlasticLicensingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlasticLicensingApp.Services
{
    public class DatabaseInitializationService : IDatabaseInitializationService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<DatabaseInitializationService> _logger;

        public DatabaseInitializationService(ApplicationDbContext context, ILogger<DatabaseInitializationService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task InitializeDatabaseAsync()
        {
            try
            {
                await _context.Database.MigrateAsync();

                // 1. Seed Roles
                if (!await _context.Roles.AnyAsync())
                {
                    _context.Roles.AddRange(
                        new Role { RoleName = "Admin", Description = "System administrator with full access", CreatedBy = 1, CreatedDate = DateTime.Now },
                        new Role { RoleName = "Supplier", Description = "Supplier user with product submission rights", CreatedBy = 1, CreatedDate = DateTime.Now }
                    );
                    await _context.SaveChangesAsync();
                }

                // 2. Seed Addresses (Initial)
                if (!await _context.Addresses.AnyAsync())
                {
                    _context.Addresses.Add(new Address 
                    { 
                        Line1 = "123 Industrial Road", 
                        City = "Chennai", 
                        State = "Tamil Nadu", 
                        Country = "India", 
                        Pincode = "600001", 
                        Phone = "+91-44-1234567", 
                        CreatedBy = 1, 
                        CreatedDate = DateTime.Now 
                    });
                    await _context.SaveChangesAsync();
                }

                // 3. Seed Users (System Admin)
                if (!await _context.Users.AnyAsync())
                {
                    var adminRole = await _context.Roles.FirstAsync(r => r.RoleName == "Admin");
                    _context.Users.Add(new User
                    {
                        FirstName = "System",
                        LastName = "Admin",
                        Email = "admin@plasticlicensing.com",
                        PasswordHash = "AQAAAAIAAYagAAAAEJ5v...", // Placeholder hash
                        RoleID = adminRole.ID,
                        AddressID = null, // Admin doesn't need a physical address initially
                        CreatedBy = 1,
                        CreatedDate = DateTime.Now
                    });
                    await _context.SaveChangesAsync();
                }

                // 4. Seed Parameters
                if (!await _context.Parameters.AnyAsync())
                {
                    var parameters = new List<string> { "Density", "Durability", "Recyclability", "Toxicity Level", "Melting Point", "Thermal Conductivity", "Flexural Strength", "Impact Resistance", "Water Absorption Rate", "UV Resistance", "Chemical Resistance", "Biodegradability" };
                    foreach (var p in parameters)
                    {
                        _context.Parameters.Add(new Parameter { ParameterName = p, IsActive = true, CreatedBy = 1, CreatedDate = DateTime.Now });
                    }
                    await _context.SaveChangesAsync();
                }

                // 5. Seed Statuses
                if (!await _context.Statuses.AnyAsync())
                {
                    var productStatuses = new List<string> { "New", "Submitted", "Approved", "Rejected", "Denied", "Withdrawn", "Response Required", "Responded" };
                    foreach (var s in productStatuses)
                    {
                        _context.Statuses.Add(new Status { StatusName = s, StatusType = "Product", IsActive = true, CreatedBy = 1, CreatedDate = DateTime.Now });
                    }
                    var licenseStatuses = new List<string> { "New", "Submitted", "Active", "Expired", "Withdrawn" };
                    foreach (var s in licenseStatuses)
                    {
                        _context.Statuses.Add(new Status { StatusName = s, StatusType = "License", IsActive = true, CreatedBy = 1, CreatedDate = DateTime.Now });
                    }
                    await _context.SaveChangesAsync();
                }

                // 6. Seed Notification Templates
                if (!await _context.NotificationTemplates.AnyAsync())
                {
                    _context.NotificationTemplates.AddRange(
                        new NotificationTemplate { TemplateName = "ProductApproved", Subject = "Your product has been approved", Body = "Dear Supplier,\n\nYour product \"{ProductName}\" has been approved.\n\nRegards,\nLicensing Team", IsActive = true, CreatedBy = 1, CreatedDate = DateTime.Now },
                        new NotificationTemplate { TemplateName = "ProductDenied", Subject = "Your product has been denied", Body = "Dear Supplier,\n\nYour product \"{ProductName}\" has been denied.\n\nRegards,\nLicensing Team", IsActive = true, CreatedBy = 1, CreatedDate = DateTime.Now },
                        new NotificationTemplate { TemplateName = "ResponseRequired", Subject = "Response required for your product", Body = "Dear Supplier,\n\nYour product \"{ProductName}\" requires additional information.\n\nRegards,\nLicensing Team", IsActive = true, CreatedBy = 1, CreatedDate = DateTime.Now },
                        new NotificationTemplate { TemplateName = "ResponseSubmitted", Subject = "Response received", Body = "Dear Admin,\n\nThe supplier has submitted a response for product \"{ProductName}\".\n\nRegards,\nLicensing System", IsActive = true, CreatedBy = 1, CreatedDate = DateTime.Now },
                        new NotificationTemplate { TemplateName = "LicenseExpiryReminder", Subject = "Your license is expiring soon", Body = "Dear Supplier,\n\nYour license \"{LicenseNumber}\" will expire on {ExpiryDate}.\n\nRegards,\nLicensing Team", IsActive = true, CreatedBy = 1, CreatedDate = DateTime.Now },
                        new NotificationTemplate { TemplateName = "LicenseActivated", Subject = "Your license is now active", Body = "Dear Supplier,\n\nYour license \"{LicenseNumber}\" has been activated.\n\nRegards,\nLicensing Team", IsActive = true, CreatedBy = 1, CreatedDate = DateTime.Now }
                    );
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while migrating or seeding the database.");
            }
        }
    }
}
