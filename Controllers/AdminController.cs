using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StayFit.Controllers
{
    [Authorize(Roles = "Admin")] // role base authorization
    public class AdminController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult ManageUsers()
        {
            return View();
        }

        public IActionResult ManagePlans()
        {
            return View();
        }

        public IActionResult ViewFeedback()
        {
            return View();
        }
    }
}
