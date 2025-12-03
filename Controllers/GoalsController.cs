using Microsoft.AspNetCore.Mvc;

namespace StayFit.Controllers
{
    public class GoalsController : Controller
    {
        [HttpGet]
        public IActionResult SelectGoal()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SelectGoal(string goal)
        {
            if (goal == "WeightLoss")
                return RedirectToAction("WeightLossPlans", "Plans");

            if (goal == "WeightGain")
                return RedirectToAction("WeightGainPlans", "Plans");

            return View();
        }
    }
}
