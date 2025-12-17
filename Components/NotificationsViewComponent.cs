using Microsoft.AspNetCore.Mvc;

namespace StayFit.Components
{
    public class NotificationsViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
