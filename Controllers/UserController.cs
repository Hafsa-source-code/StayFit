using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StayFit.Models.Domain;
using StayFit.Models.Interfaces;
using System.Security.Claims; 
using System;

namespace StayFit.Controllers
{
    [Authorize] 
    public class UserController : Controller
    {
        private readonly IProgressRepository _progressRepository;

        public UserController(IProgressRepository progressRepository)
        {
            _progressRepository = progressRepository;
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult UpdateProfile()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UpdateProfile(string name, int age, float weight)
        {
            return RedirectToAction("Dashboard");
        }

        public IActionResult ProgressTracker()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ProgressTracker(float weeklyWeight, string notes)
        {
    
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User is not authenticated.");
            }
            var progress = new Progress
            {
                UserId = userId,
                Weight = weeklyWeight,
                Date = DateTime.Now
            };

            _progressRepository.Add(progress);

            var options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(1),
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict
            };
            Response.Cookies.Append("LastProgressWeight", weeklyWeight.ToString(), options);
            Response.Cookies.Append("LastProgressDate", DateTime.Now.ToString("yyyy-MM-dd"), options);
            HttpContext.Session.SetString("FlashMessage", "Your progress has been saved successfully!");

            return RedirectToAction("Dashboard");
        }

        public IActionResult Feedback()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Feedback(string message)
        {
            HttpContext.Session.SetString("FlashMessage", "Thank you for your feedback!");
            return RedirectToAction("Dashboard");
        }
        [HttpGet]
public JsonResult GetCalorieSuggestion(int age, string gender)
{
    int calories = 0;

    if (gender.ToLower() == "male")
    {
        if (age <= 18) calories = 2500;
        else if (age <= 40) calories = 2700;
        else calories = 2400;
    }
    else if (gender.ToLower() == "female")
    {
        if (age <= 18) calories = 2000;
        else if (age <= 40) calories = 2200;
        else calories = 2000;
    }

    return Json(new { calories = calories });
}
    }
}
