using Microsoft.AspNetCore.Mvc;

namespace StayFit.Controllers
{
    public class PlansController : Controller
    {
        public IActionResult WeightLossPlans()
        {
            return View();
        }

        public IActionResult WeightGainPlans()
        {
            return View();
        }

        public IActionResult WeightLossWorkout()
        {
            return View();
        }

        public IActionResult WeightLossDiet()
        {
            return View();
        }

        public IActionResult WeightGainWorkout()
        {
            return View();
        }

        public IActionResult WeightGainDiet()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ContinueToDashboard()
        {
            return RedirectToAction("Dashboard", "User");
        }
    }
}
