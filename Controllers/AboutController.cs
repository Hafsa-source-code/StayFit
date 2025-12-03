using Microsoft.AspNetCore.Mvc;

namespace StayFit.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult About()
        {
            return View(); 
        }

    }
}
