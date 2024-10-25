using GraysonGains.Data;
using GraysonGains.Models;
using Microsoft.AspNetCore.Mvc;
using GraysonGains.Models.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace GraysonGains.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly AppDbContext dbContext;
        private readonly IConfiguration configuration;
        public AuthenticationController(AppDbContext dbContext, IConfiguration configuration)
        {
            this.dbContext = dbContext;
            this.configuration = configuration;
        }

        // All 3 HttpGet's have checks in place for if the user decides they want to try and change the URL when they don't have access.

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
        [HttpGet]
        public IActionResult CompleteRegistration()
        {
            // This TempData is set throughout the registration and login processes.
            bool formOneComplete = TempData["FormOne"] != null && (bool)TempData.Peek("FormOne");
            if (User.Identity.IsAuthenticated || !formOneComplete)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(UsersViewModel usersViewModel)
        {
            // Hash the password
            var hashedPassword = HashPassword(usersViewModel.PasswordHash);

            // Call helpers for server-side username and email checks
            if (await DuplicateUsername(usersViewModel.Username)) {
                TempData["ErrorMessage"] = "This username already exists. Try again.";
                return RedirectToAction("Register", "Authentication");
            }

            if (await DuplicateEmail(usersViewModel.Email))
            {
                TempData["ErrorMessage"] = "This email already exists. Try again.";
                return RedirectToAction("Register", "Authentication");
            }

            // Apply to DB
            var user = new Users
            {
                FirstName = usersViewModel.FirstName,
                LastName = usersViewModel.LastName,
                Email = usersViewModel.Email,
                Username = usersViewModel.Username,
                PasswordHash = hashedPassword,
                Gender = usersViewModel.Gender,
                HeightFeet = usersViewModel.HeightFeet,
                HeightInches = usersViewModel.HeightInches,
                Weight = usersViewModel.Weight,
            };
            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();

            TempData["UserId"] = user.UserId.ToString();
            TempData["FormOne"] = true;

            return RedirectToAction("CompleteRegistration", "Authentication");
        }
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            // Finds first instance of username
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);

            // Checks for username and password to compare w DB
            if (user == null)
            {
                user = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == username);
            }
            if (user == null)
            {
                ViewBag.ErrorMessage = "Invalid username/email or password.";
                return View();
            }
            TempData["UserId"] = user.UserId.ToString();

            if (!VerifyPassword(password, user.PasswordHash))
            {
                ViewBag.ErrorMessage = "Invalid username/email or password.";
                return View();
            }

            // Helpers for checking forms
            if (await NotFullyRegisteredFormOne(TempData["UserId"].ToString()))
            {
                TempData["FormOne"] = true;
                return RedirectToAction("CompleteRegistration", "Authentication");
            }

            if (await NotFullyRegisteredFormTwo(TempData["UserId"].ToString()))
            {
                TempData["FormOne"] = true;
                TempData["CurrentForm"] = "form2";
                return RedirectToAction("CompleteRegistration", "Authentication");
            }

            // Log user in
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

            TempData.Remove("UserId");

            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Authentication");
        }
        [HttpPost]
        public async Task<IActionResult> RegisterPRsDB(UserPRsViewModel userPRsViewModel)
        {
            Guid.TryParse(TempData["UserId"].ToString(), out Guid userId);

            var userPR = new UserPRs
            {
                UserId = userId,
                BenchPR = userPRsViewModel.BenchPR,
                SquatPR = userPRsViewModel.SquatPR,
                DLPR = userPRsViewModel.DLPR,
                SPPR = userPRsViewModel.SPPR,

            };
            await dbContext.UserPRs.AddAsync(userPR);
            await dbContext.SaveChangesAsync();

            // Setting variables for page/form checks
            TempData["FormOne"] = true;
            TempData["CurrentForm"] = "form2";
            return RedirectToAction("CompleteRegistration", "Authentication");
        }
        [HttpPost]
        public async Task<IActionResult> RegisterDatesDB(UserWorkoutDaysViewModel userWorkoutDaysViewModel)
        {
            Guid.TryParse(TempData["UserId"].ToString(), out Guid userId);

            // Check for duplicate days
            var selectedDays = new List<string> { userWorkoutDaysViewModel.BenchDay, userWorkoutDaysViewModel.SquatDay, userWorkoutDaysViewModel.DLDay, userWorkoutDaysViewModel.SPDay };

            if (selectedDays.Count != selectedDays.Distinct().Count())
            {
                // Setting variables for page/form checks
                TempData["CurrentForm"] = "form2";
                TempData["ErrorMessage"] = "Each workout day must be unique. Please select different days for each workout.";
                return RedirectToAction("CompleteRegistration", "Authentication");
            }

            var workoutDay = new UserWorkoutDays
            {
                UserId = userId,
                BenchDay = userWorkoutDaysViewModel.BenchDay,
                SquatDay = userWorkoutDaysViewModel.SquatDay,
                DLDay = userWorkoutDaysViewModel.DLDay,
                SPDay = userWorkoutDaysViewModel.SPDay,
                CycleStart = userWorkoutDaysViewModel.CycleStart,
            };
            await dbContext.UserWorkoutDays.AddAsync(workoutDay);
            await dbContext.SaveChangesAsync();

            // Sign the user in after all registration steps are complete
            var user = await dbContext.Users.FindAsync(userId);
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
        private string HashPassword(string password)
        {
            // Hash password using BCrypt with a work factor of 12, a pepper from an env variable
            var pepper = configuration["AppSettings:Pepper"];
            return BCrypt.Net.BCrypt.HashPassword(password + pepper, workFactor: 12);
        }
        private bool VerifyPassword(string enteredPassword, string storedHash)
        {
            var pepper = configuration["AppSettings:Pepper"];
            return BCrypt.Net.BCrypt.Verify(enteredPassword + pepper, storedHash);
        }
        private async Task<bool> NotFullyRegisteredFormOne(string userIDString)
        {
            Guid.TryParse(TempData["UserId"].ToString(), out Guid userId);
            if (await dbContext.UserPRs.SingleOrDefaultAsync(u => u.UserId == userId) == null)
            {
                return true;
            }
            return false;
        }
        private async Task<bool> NotFullyRegisteredFormTwo(string userIDString)
        {
            Guid.TryParse(TempData["UserId"].ToString(), out Guid userId);
            if (await dbContext.UserWorkoutDays.SingleOrDefaultAsync(u => u.UserId == userId) == null)
            {
                return true;
            }
            return false;
        }
        private async Task<bool> DuplicateUsername(string username)
        {
            if (await dbContext.Users.SingleOrDefaultAsync(u => u.Username == username) != null)
            {
                return true;
            }
            return false;
        }
        private async Task<bool> DuplicateEmail(string email)
        {
            if (await dbContext.Users.SingleOrDefaultAsync(u => u.Email == email) != null)
            {
                return true;
            }
            return false;
        }
    }
}
