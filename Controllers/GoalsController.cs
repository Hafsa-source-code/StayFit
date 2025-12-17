using StayFit.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StayFit.Data;
using StayFit.Models.Domain;

namespace StayFit.Controllers
{
    public class GoalsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public GoalsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // ✅ AJAX PARTIAL LOAD
        [HttpGet]
        public async Task<IActionResult> SelectGoalPartial()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var profile = _context.UserProfiles
                .FirstOrDefault(p => p.UserId == user.Id);
            string currentGoal = profile?.GoalType ?? "";
            return PartialView("_SelectGoal", currentGoal);
        }

        // ✅ SAVE GOAL VIA AJAX
        [HttpPost]
        public async Task<IActionResult> SaveGoal([FromBody] GoalInputModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var profile = _context.UserProfiles
                .FirstOrDefault(p => p.UserId == user.Id);

            if (profile == null)
                return BadRequest("Profile not found");

            profile.GoalType = model.Goal;
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }
    }

    public class GoalInputModel
    {
        public string Goal { get; set; }
    }
}
