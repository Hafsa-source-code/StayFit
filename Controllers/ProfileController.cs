using StayFit.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StayFit.Data;
using StayFit.Models.Domain;
using System.Security.Claims;

namespace StayFit.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public ProfileController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // Full-page view for first-time signup profile completion
        [HttpGet]
        public async Task<IActionResult> Setup()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");

            var profile = _context.UserProfiles.FirstOrDefault(p => p.UserId == user.Id)
                          ?? new UserProfile { UserId = user.Id };

            return View("Setup", profile); // full-page view
        }

        // Partial view for dashboard AJAX
        [HttpGet]
        public async Task<IActionResult> SetupPartial()
        {
            var user = await _userManager.GetUserAsync(User);
        if (user == null) return Unauthorized();

        var profile = _context.UserProfiles.FirstOrDefault(p => p.UserId == user.Id)
                    ?? new UserProfile { UserId = user.Id };

        return PartialView("_Setup", profile);
        }

        // Save profile via AJAX or full-page POST
        [HttpPost]
        public async Task<IActionResult> SaveProfile([FromBody] UserProfile model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var profile = _context.UserProfiles.FirstOrDefault(p => p.UserId == user.Id);

            if (profile == null)
            {
                model.UserId = user.Id;
                _context.UserProfiles.Add(model);
            }
            else
            {
                profile.FullName = model.FullName;
                profile.Age = model.Age;
                profile.Weight = model.Weight;
                profile.Height = model.Height;
                profile.Gender = model.Gender;
                profile.GoalType = model.GoalType;
            }

            await _context.SaveChangesAsync();
            return Json(new { success = true, message = "Profile saved successfully!" });
        }
    }
}
