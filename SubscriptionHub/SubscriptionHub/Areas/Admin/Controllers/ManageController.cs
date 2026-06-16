using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SubscriptionHub.Data;
using SubscriptionHub.Models;

namespace SubscriptionHub.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ManageController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ManageController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult DeleteUser(int id)
        {
            _context.Users.Remove(_context.Users.Find(id));
            _context.SaveChanges();
            return RedirectToAction("ManageUsers", "Manage");

        }
        public IActionResult ManageUsers()
        {
            var users = _context.Users.ToList();
            return View(users);
        }

        public IActionResult ManageServices()
        {
            var services = _context.Services.ToList();
            return View(services);
        }

        public IActionResult ManageCategories()
        {
            var categories = _context.Categories.ToList();
            return View(categories);
        }

        public IActionResult ManageSubscriptions()
        {
            ViewBag.Services = _context.Services.ToList();

            var subscriptions = _context.Subscriptions
                .Include(s => s.User)
                .Include(s => s.Service)
                .ToList();

            return View(subscriptions);
        }
    }
}