using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StayFit.Data;
using System;
using StayFit.Models.Domain;
using Microsoft.AspNetCore.SignalR;
using StayFit.Hubs;

namespace StayFit.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
          private readonly IHubContext<NotificationHub> _hubContext;

     public AdminController(ApplicationDbContext context, IHubContext<NotificationHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        public async Task<IActionResult> ManageUsers()
        {
            var users = await (
                from u in _context.Users
                join p in _context.UserProfiles
                    on u.Id equals p.UserId into profileGroup
                from pg in profileGroup.DefaultIfEmpty()
                select new
                {
                    UserId = u.Id,
                    Email = u.Email,
                    FullName = pg != null ? pg.FullName : "N/A",
                    GoalType = pg != null ? pg.GoalType : "N/A"
                }
            ).ToListAsync();

            ViewBag.Users = users;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound();

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return RedirectToAction("ManageUsers");
        }
        public IActionResult ManagePlans()
        {
            return View();
        }

        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _context.Users
                .Where(u => u.Id == id)
                .Select(u => new
                {
                    u.Id,
                    u.Email,
                    Profile = _context.UserProfiles.FirstOrDefault(p => p.UserId == u.Id)
                })
                .FirstOrDefaultAsync();

            if (user == null)
                return NotFound();

            ViewBag.UserId = user.Id;
            ViewBag.Email = user.Email;
            ViewBag.FullName = user.Profile?.FullName;
            ViewBag.GoalType = user.Profile?.GoalType;

            return View();
        }
            [HttpPost]
        public async Task<IActionResult> EditUser(
            string id,
            string email,
            string fullName,
            string goalType)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound();

            // Update AspNetUsers
            user.Email = email;
            user.UserName = email;

            // Update UserProfile
            var profile = await _context.UserProfiles
                .FirstOrDefaultAsync(p => p.UserId == id);

            if (profile != null)
            {
                profile.FullName = fullName;
                profile.GoalType = goalType;
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("ManageUsers");
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

        [HttpPost]
    public async Task<IActionResult> SendAnnouncement(string message)
    {
        if (string.IsNullOrWhiteSpace(message))
            return Json(new { success = false });

        var announcement = new Announcement
        {
            Message = message,
            DateCreated = DateTime.UtcNow
        };
        _context.Announcements.Add(announcement);
        await _context.SaveChangesAsync();

        // Send to all connected users
        await _hubContext.Clients.All.SendAsync("ReceiveNotification", message);

        return Json(new { success = true });
    }

    }
}
