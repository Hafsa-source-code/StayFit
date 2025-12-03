using Microsoft.AspNetCore.Mvc;

namespace StayFit.Controllers
{
    public class ProfileController : Controller
    {
        [HttpGet]
        public IActionResult Setup()
        {
            return View(); 
        }

        [HttpPost]
        public IActionResult Setup(string name, int age, float weight, float height)
        {
            return RedirectToAction("SelectGoal", "Goals");
        }
    }
}
