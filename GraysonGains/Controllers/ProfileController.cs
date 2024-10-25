using GraysonGains.Data;
using GraysonGains.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GraysonGains.Models;
using GraysonGains.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace GraysonGains.Controllers
{
    public class ProfileController : Controller
    {
        private readonly AppDbContext dbContext;
        private readonly IConfiguration configuration;
        public ProfileController(AppDbContext dbContext, IConfiguration configuration)
        {
            this.dbContext = dbContext;
            this.configuration = configuration;
        }
        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            var user = await dbContext.Users
                .SingleOrDefaultAsync(u => u.Username == User.Identity.Name);
            return View(user);
        }
        [HttpGet]
        public async Task<IActionResult> EditPRs()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Guid.TryParse(userIdString, out Guid userId);

            var userPRs = await dbContext.UserPRs
                .SingleOrDefaultAsync(u => u.UserId == userId);
            return View(userPRs);
        }
        [HttpGet]
        public async Task<IActionResult> EditCycleDays()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Guid.TryParse(userIdString, out Guid userId);

            var userCycleDays = await dbContext.UserWorkoutDays
                .SingleOrDefaultAsync(u => u.UserId == userId);
            return View(userCycleDays);
        }
        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> EditProfileDB(Users userEntity)
        {
            var user = await dbContext.Users
                    .SingleOrDefaultAsync(u => u.Username == User.Identity.Name);

            // Sign user out to update their profile
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            if (user is not null)
            {
                user.FirstName = userEntity.FirstName;
                user.LastName = userEntity.LastName;
                user.Email = userEntity.Email;
                user.Username = userEntity.Username;
                user.Gender = userEntity.Gender;
                user.HeightFeet = userEntity.HeightFeet;
                user.HeightInches = userEntity.HeightInches;
                user.Weight = userEntity.Weight;
                await dbContext.SaveChangesAsync();
            }

            // Sign user back in
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName), 
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString())
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));

            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> EditPRsDB(UserPRs userEntity)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Guid.TryParse(userIdString, out Guid userId);

            var userPRs = await dbContext.UserPRs
                .SingleOrDefaultAsync(u => u.UserId == userId);

            if (userPRs is not null)
            {
                userPRs.BenchPR = userEntity.BenchPR;
                userPRs.SquatPR = userEntity.SquatPR;
                userPRs.DLPR = userEntity.DLPR;
                userPRs.SPPR = userEntity.SPPR;
     
                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> EditCycleDaysDB(UserWorkoutDays userEntity)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Guid.TryParse(userIdString, out Guid userId);

            var userPRs = await dbContext.UserWorkoutDays
                .SingleOrDefaultAsync(u => u.UserId == userId);

            // Check for duplicate days
            var selectedDays = new List<string> { userEntity.BenchDay, userEntity.SquatDay, userEntity.DLDay, userEntity.SPDay };

            if (selectedDays.Count != selectedDays.Distinct().Count())
            {
                TempData["ErrorMessage"] = "Each workout day must be unique. Please select different days for each workout.";
                return RedirectToAction("EditCycleDays", "Profile");
            }

            if (userPRs is not null)
            {
                userPRs.BenchDay = userEntity.BenchDay;
                userPRs.SquatDay = userEntity.SquatDay;
                userPRs.DLDay = userEntity.DLDay;
                userPRs.SPDay = userEntity.SPDay;

                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> ChangePasswordDB(string currentPassword, string newPassword)
        {
            var user = await dbContext.Users
                    .SingleOrDefaultAsync(u => u.Username == User.Identity.Name);

            // Handle the case where the user is not found
            if (user is null)
            {
                return NotFound(); 
            }

            // Check if the current password is correct
            var pepper = configuration["AppSettings:Pepper"];
            var passwordCheck = BCrypt.Net.BCrypt.Verify(currentPassword + pepper, user.PasswordHash);
            if (!passwordCheck)
            {
                // Send error message w TempData to alert user from server
                TempData["ErrorMessage"] = "Current password is incorrect.";
                return RedirectToAction("ChangePassword", "Profile");
            }

            // Hash the new password
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword + pepper);

            await dbContext.SaveChangesAsync();

            // Relog the user
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString())
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProfileDB()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Guid.TryParse(userIdString, out Guid userId);

            var user = await dbContext.Users.FindAsync(userId);
            var userPR = await dbContext.UserPRs.SingleOrDefaultAsync(pr => pr.UserId == userId);
            var userWorkoutDays = await dbContext.UserWorkoutDays.SingleOrDefaultAsync(ud => ud.UserId == userId);

            // Remove user from all 3 tables
            if (user is not null)
            {
                dbContext.Users.Remove(user);
            }
            if (userPR is not null)
            {
                dbContext.UserPRs.Remove(userPR);
            }
            if (userWorkoutDays is not null)
            {
                dbContext.UserWorkoutDays.Remove(userWorkoutDays);
            }
            await dbContext.SaveChangesAsync();
            return RedirectToAction("Login", "Authentication");
        }
    }
}
