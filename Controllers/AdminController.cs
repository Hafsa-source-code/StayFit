using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StayFit.Data;
using StayFit.Models.Domain;

namespace StayFit.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

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

        public async Task<IActionResult> ViewFeedback()
        {
            // join Feedbacks with UserProfiles on UserId
    var feedbacks = await (from f in _context.Feedbacks
                           join u in _context.UserProfiles
                           on f.UserId equals u.UserId into userGroup
                           from ug in userGroup.DefaultIfEmpty()
                           orderby f.Date descending
                           select new
                           {
                               f.Id,
                               UserEmail = f.User!.Email, // from ApplicationUser
                               UserFullName = ug != null ? ug.FullName : null,
                               f.Message,
                               f.Date
                           }).ToListAsync();

            return View(feedbacks);
        }
    }
}
