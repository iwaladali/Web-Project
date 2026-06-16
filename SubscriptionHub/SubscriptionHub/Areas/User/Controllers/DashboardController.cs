using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SubscriptionHub.Data;

namespace SubscriptionHub.Areas.User.Controllers
{
    [Area("User")]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var user_id = HttpContext.Session.GetInt32("ID");
            if (user_id == null)
                return RedirectToAction("Login", "Account");

            var user= _context.Users.Include(u=> u.Subscriptions).ThenInclude(s=>s.Service).FirstOrDefault(u=>u.UserID==user_id);
           

            return View(user);
        }
    }
}