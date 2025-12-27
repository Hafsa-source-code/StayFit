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
