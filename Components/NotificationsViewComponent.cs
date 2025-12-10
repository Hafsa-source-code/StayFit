using Microsoft.AspNetCore.Mvc;

namespace StayFit.Components
{
    public class NotificationsViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            // Example notification list
            var notifications = new List<string>
            {
                "New user registered",
                "3 pending bookings",
                "System update available"
            };

            return View(notifications);  // Pass data to Default.cshtml
        }
    }
}
