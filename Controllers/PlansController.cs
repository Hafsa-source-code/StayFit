using StayFit.Data;
using StayFit.Models;
using StayFit.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using StayFit.Hubs;
using Microsoft.EntityFrameworkCore;

public class PlansController : Controller
{
    private readonly IHubContext<NotificationHub> _hubContext;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public PlansController(
        IHubContext<NotificationHub> hubContext,
        ApplicationDbContext context,
        UserManager<ApplicationUser> userManager)
    {
        _hubContext = hubContext;
        _context = context;
        _userManager = userManager;
    }

    // Partial Views for individual plans
    public async Task<IActionResult> WeightLossWorkout()
    {
        var workouts = await _context.Workouts
            .Where(w => w.Level == "WeightLoss")
            .ToListAsync();
        return PartialView("_WeightLossWorkout", workouts);
    }

    public async Task<IActionResult> WeightLossDiet()
    {
        var diets = await _context.DietPlans
            .Where(d => d.GoalType == "WeightLoss")
            .ToListAsync();
        return PartialView("_WeightLossDiet", diets);
    }

    public async Task<IActionResult> WeightGainWorkout()
    {
        var workouts = await _context.Workouts
            .Where(w => w.Level == "WeightGain")
            .ToListAsync();
        return PartialView("_WeightGainWorkout", workouts);
    }

    public async Task<IActionResult> WeightGainDiet()
    {
        var diets = await _context.DietPlans
            .Where(d => d.GoalType == "WeightGain")
            .ToListAsync();
        return PartialView("_WeightGainDiet", diets);
    }

    // Load the appropriate bundle based on user's goal
    [HttpGet]
    public async Task<IActionResult> LoadGoalPlans()
    {
        var user = await _userManager.GetUserAsync(User);
        var profile = await _context.UserProfiles
            .FirstOrDefaultAsync(p => p.UserId == user.Id);

        if (profile == null || string.IsNullOrEmpty(profile.GoalType))
            return PartialView("_NoGoalSelected");

        if (profile.GoalType == "WeightGain")
        {
            var dietPlans = await _context.DietPlans
                .Where(d => d.GoalType == "WeightGain")
                .ToListAsync();
            var workouts = await _context.Workouts
                .Where(w => w.Level == "WeightGain")
                .ToListAsync();

            ViewData["DietPlans"] = dietPlans;
            ViewData["Workouts"] = workouts;

            return PartialView("_WeightGainBundle");
        }
        else // WeightLoss
        {
            var dietPlans = await _context.DietPlans
                .Where(d => d.GoalType == "WeightLoss")
                .ToListAsync();
            var workouts = await _context.Workouts
                .Where(w => w.Level == "WeightLoss")
                .ToListAsync();

            ViewData["DietPlans"] = dietPlans;
            ViewData["Workouts"] = workouts;

            return PartialView("_WeightLossBundle");
        }
    }

    [HttpPost]
public async Task<IActionResult> SaveUserPlan([FromBody] PlanInputModel model)
{
    var user = await _userManager.GetUserAsync(User);
    if (user == null) return Unauthorized();

    var profile = await _context.UserProfiles
        .FirstOrDefaultAsync(p => p.UserId == user.Id);
    if (profile == null) return BadRequest("Profile not found");

    profile.GoalType = model.PlanType;
    await _context.SaveChangesAsync();

    // Optional: send real-time notification
    await _hubContext.Clients.All.SendAsync(
        "ReceiveNotification",
        $"📌 {user.Email} selected {model.PlanType} plan"
    );

    return Ok(new { success = true });
}

public class PlanInputModel
{
    public string? PlanType { get; set; }
}
}
