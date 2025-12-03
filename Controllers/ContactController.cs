using Microsoft.AspNetCore.Mvc;

namespace StayFit.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Contact()
        {
            return View();
        }

    }
}
