using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticLicensingApp.Data;
using PlasticLicensingApp.Models;
using PlasticLicensingApp.ViewModels;
using System.Security.Claims;

namespace PlasticLicensingApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid input data.");

            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == model.Email && u.PasswordHash == model.Password);

            if (user == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()),
                new Claim(ClaimTypes.Role, user.Role?.RoleName ?? "User")
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            return Ok(new { redirectUrl = Url.Action("Index", "Supplier") });
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest("Invalid data");

            if (await _context.Users.AnyAsync(u => u.Email == model.Email))
                return BadRequest("Email already exists");

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

            return Ok();
        }

        public IActionResult RegistrationSuccess() => View();

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}