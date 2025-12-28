using Microsoft.AspNetCore.Mvc;
using StayFit.Data;
using StayFit.Models.Domain;
using Microsoft.AspNetCore.Identity;
using StayFit.Models;
using StayFit.Models.ViewModels;

public class UserController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _context;

    public UserController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    public async Task<IActionResult> Dashboard()
    {
        var user = await _userManager.GetUserAsync(User);
    if (user == null) return RedirectToAction("Login", "Account");

    var profile = _context.UserProfiles.FirstOrDefault(p => p.UserId == user.Id);
    string userName = profile?.FullName ?? user.Email; 

    ViewData["UserName"] = userName;
    ViewData["GoalType"] = profile?.GoalType;

        return View();
    }

    [HttpGet]
    public async Task<IActionResult> UserGoalPartial()
    {
        var user = await _userManager.GetUserAsync(User);
        var profile = _context.UserProfiles.FirstOrDefault(p => p.UserId == user.Id);
        return PartialView("_UserGoalPartial", profile?.GoalType);
    }
       [HttpGet]
    public IActionResult FeedbackPartial()
    {
        return PartialView("_Feedback",User); // _Feedback.cshtml
    }

    [HttpPost]
public async Task<IActionResult> Feedback(string message)
{
    if (string.IsNullOrWhiteSpace(message))
        return Json(new { success = false, message = "Message cannot be empty." });

    var user = await _userManager.GetUserAsync(User);
    if (user == null) return Unauthorized();

    var feedback = new Feedback
    {
        UserId = user.Id,
        Message = message,
        Date = DateTime.UtcNow
    };

    _context.Feedbacks.Add(feedback);
    await _context.SaveChangesAsync();

    return Json(new { success = true, message = "Feedback submitted successfully!" });
}

   [HttpGet]
public IActionResult ProgressTrackerPartial()
{
    return PartialView("_ProgressTracker");
}

        // POST: Save progress
        [HttpPost]
        public async Task<IActionResult> ProgressTracker([FromForm] double weeklyWeight)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var progress = new Progress
            {
                UserId = user.Id,
                Weight = weeklyWeight,
                Date = DateTime.Now
            };

            _context.ProgressRecords.Add(progress);
            await _context.SaveChangesAsync();

            TempData["FlashMessage"] = "Progress saved successfully!";
            return RedirectToAction("Dashboard"); 
        }

        [HttpGet]
public async Task<IActionResult> PlanWidgetsPartial()
{
    var user = await _userManager.GetUserAsync(User);
    if (user == null) return Unauthorized();

    var profile = _context.UserProfiles
        .FirstOrDefault(p => p.UserId == user.Id);

    if (profile == null || string.IsNullOrEmpty(profile.GoalType))
        return PartialView("_NoGoalSelected");

    var vm = new DashboardPlanWidgetVM
    {
        DietPlans = _context.DietPlans
            .Where(d => d.GoalType == profile.GoalType)
            .ToList(),

        Workouts = _context.Workouts
            .Where(w => w.Level == profile.GoalType)
            .ToList()
    };

    return PartialView("_DashboardWidgets", vm);
}

    [HttpGet]
public async Task<IActionResult> GetNotificationCount()
{
    var user = await _userManager.GetUserAsync(User);
    if (user == null) return Unauthorized();

    // TEMP: hardcoded value
    // Later replace with database count
    int count = 3;

    return Json(count);
}
[HttpGet]
public IActionResult NotificationsPartial()
{
    var notifications = _context.Announcements
        .OrderByDescending(a => a.DateCreated)
        .Take(20) // last 20 notifications
        .ToList();

    return PartialView("_Notification", notifications);
}
[HttpGet]
public IActionResult GetCalorieSuggestion(int age, string gender)
{
    if (age <= 0 || string.IsNullOrEmpty(gender))
        return Json(new { calories = 0 });
    double calories;
    if (gender.ToLower() == "male")
        calories = 10 * 70 + 6.25 * 170 - 5 * age + 5; 
    else
        calories = 10 * 60 + 6.25 * 160 - 5 * age - 161; 

    return Json(new { calories = Math.Round(calories) });
}



        }