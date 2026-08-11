using Microsoft.EntityFrameworkCore;
using PlasticLicensingApp.Data;
using PlasticLicensingApp.Models;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Database Migration and Seeding
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        context.Database.Migrate();

        // 1. Seed Roles
        if (!context.Roles.Any())
        {
            context.Roles.AddRange(
                new Role { RoleName = "Admin", Description = "System administrator with full access", CreatedBy = 1, CreatedDate = DateTime.Now },
                new Role { RoleName = "Supplier", Description = "Supplier user with product submission rights", CreatedBy = 1, CreatedDate = DateTime.Now }
            );
            context.SaveChanges();
        }

        // 2. Seed Addresses (Initial)
        if (!context.Addresses.Any())
        {
            context.Addresses.Add(new Address 
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
            context.SaveChanges();
        }

        // 3. Seed Users (System Admin)
        if (!context.Users.Any())
        {
            var adminRole = context.Roles.First(r => r.RoleName == "Admin");
            context.Users.Add(new User
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
            context.SaveChanges();
        }

        // 4. Seed Parameters
        if (!context.Parameters.Any())
        {
            var parameters = new List<string> { "Density", "Durability", "Recyclability", "Toxicity Level", "Melting Point", "Thermal Conductivity", "Flexural Strength", "Impact Resistance", "Water Absorption Rate", "UV Resistance", "Chemical Resistance", "Biodegradability" };
            foreach (var p in parameters)
            {
                context.Parameters.Add(new Parameter { ParameterName = p, IsActive = true, CreatedBy = 1, CreatedDate = DateTime.Now });
            }
            context.SaveChanges();
        }

        // 5. Seed Statuses
        if (!context.Statuses.Any())
        {
            var productStatuses = new List<string> { "New", "Submitted", "Approved", "Rejected", "Denied", "Withdrawn", "Response Required", "Responded" };
            foreach (var s in productStatuses)
            {
                context.Statuses.Add(new Status { StatusName = s, StatusType = "Product", IsActive = true, CreatedBy = 1, CreatedDate = DateTime.Now });
            }
            var licenseStatuses = new List<string> { "New", "Submitted", "Active", "Expired", "Withdrawn" };
            foreach (var s in licenseStatuses)
            {
                context.Statuses.Add(new Status { StatusName = s, StatusType = "License", IsActive = true, CreatedBy = 1, CreatedDate = DateTime.Now });
            }
            context.SaveChanges();
        }

        // 6. Seed Notification Templates
        if (!context.NotificationTemplates.Any())
        {
            context.NotificationTemplates.AddRange(
                new NotificationTemplate { TemplateName = "ProductApproved", Subject = "Your product has been approved", Body = "Dear Supplier,\n\nYour product \"{ProductName}\" has been approved.\n\nRegards,\nLicensing Team", IsActive = true, CreatedBy = 1, CreatedDate = DateTime.Now },
                new NotificationTemplate { TemplateName = "ProductDenied", Subject = "Your product has been denied", Body = "Dear Supplier,\n\nYour product \"{ProductName}\" has been denied.\n\nRegards,\nLicensing Team", IsActive = true, CreatedBy = 1, CreatedDate = DateTime.Now },
                new NotificationTemplate { TemplateName = "ResponseRequired", Subject = "Response required for your product", Body = "Dear Supplier,\n\nYour product \"{ProductName}\" requires additional information.\n\nRegards,\nLicensing Team", IsActive = true, CreatedBy = 1, CreatedDate = DateTime.Now },
                new NotificationTemplate { TemplateName = "ResponseSubmitted", Subject = "Response received", Body = "Dear Admin,\n\nThe supplier has submitted a response for product \"{ProductName}\".\n\nRegards,\nLicensing System", IsActive = true, CreatedBy = 1, CreatedDate = DateTime.Now },
                new NotificationTemplate { TemplateName = "LicenseExpiryReminder", Subject = "Your license is expiring soon", Body = "Dear Supplier,\n\nYour license \"{LicenseNumber}\" will expire on {ExpiryDate}.\n\nRegards,\nLicensing Team", IsActive = true, CreatedBy = 1, CreatedDate = DateTime.Now },
                new NotificationTemplate { TemplateName = "LicenseActivated", Subject = "Your license is now active", Body = "Dear Supplier,\n\nYour license \"{LicenseNumber}\" has been activated.\n\nRegards,\nLicensing Team", IsActive = true, CreatedBy = 1, CreatedDate = DateTime.Now }
            );
            context.SaveChanges();
        }
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating or seeding the database.");
    }
}

app.Run();
