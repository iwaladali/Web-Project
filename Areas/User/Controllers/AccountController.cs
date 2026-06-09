using Microsoft.AspNetCore.Mvc;

namespace SubscriptionHub.Areas.User.Controllers
{
    [Area("User")]
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Profile()
        {
            var user = new SubscriptionHub.Models.User
            {
                FirstName = "Sara",
                LastName = "Ali",
                Email = "sara@gmail.com",
                isAdmin = false
            };

            return View(user);
        }
    }
}