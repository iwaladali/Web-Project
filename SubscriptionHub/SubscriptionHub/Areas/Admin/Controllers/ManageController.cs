using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Microsoft.EntityFrameworkCore;
using SubscriptionHub.Data;
using SubscriptionHub.Models;
using SubscriptionHub.Models.DTO;

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
            var user_id = HttpContext.Session.GetInt32("ID");
            if (user_id == null)
                return RedirectToAction("Login", "Account", new
                {
                    area = "User"
                });
            var users = _context.Users.ToList();
            return View(users);
        }

        public IActionResult RemoveSubscriptionsByService(int serviceId)
        {
            var removed_subs = _context.Subscriptions               
                .Where(sub => sub.ServiceID == serviceId).ToList();
            foreach(var sub in removed_subs) 
            _context.Subscriptions.Remove(sub);
            _context.SaveChanges();
            return RedirectToAction("ManageSubscriptions");
        }
        public IActionResult DeleteCategory(int id )
        {
            _context.Categories.Remove(_context.Categories.Find(id));   
            _context.SaveChanges(); 
            return RedirectToAction("ManageCategories");
        }
        public IActionResult AddCategory(Category req)
        {
           var cat= _context.Categories.FirstOrDefault(cat=>cat.Name.ToLower() == req.Name.ToLower());
            if (cat == null)
            {
            _context.Categories.Add(req);   
            _context.SaveChanges();
            }
            return RedirectToAction("ManageCategories");
        }
        public IActionResult EditServicePrice(EditServicePrice req)
        {
            var servic = _context.Services.Find(req.ServiceID);
            servic.Name = req.Name;
            servic.Price = req.Price;
            _context.SaveChanges();
            return RedirectToAction("ManageServices");
        }
        public IActionResult DeleteService(int id)
        {
            _context.Services.Remove(_context.Services.Find(id));  
            _context.SaveChanges();
            return RedirectToAction("ManageServices");
        }
        public IActionResult AddService(Service req)
        {

            var cat = _context.Services.FirstOrDefault(ser => ser.Name.ToLower() == req.Name.ToLower());
            if (cat == null)
            {
                _context.Services.Add(req);
                _context.SaveChanges();
            }
            return RedirectToAction("ManageServices");
        }
        public IActionResult ManageServices()
        {
            var user_id = HttpContext.Session.GetInt32("ID");
            if (user_id == null)
                return RedirectToAction("Login", "Account", new
                {
                    area = "User"
                });
            var services = _context.Services.ToList();
            ViewBag.categories= _context.Categories.ToList();
            return View(services);
        }

        public IActionResult ManageCategories()
        {
            var user_id = HttpContext.Session.GetInt32("ID");
            if (user_id == null)
                return RedirectToAction("Login", "Account", new
                {
                    area = "User"
                });
            var categories = _context.Categories.ToList();
            return View(categories);
        }

        public IActionResult MakeUserAdmin(int id)
        {
           var user= _context.Users.Find(id);
            user.isAdmin = !user.isAdmin;
            _context.SaveChanges();
            return RedirectToAction("ManageUsers", "Manage");
        }
        public IActionResult ManageSubscriptions()
        {
            var user_id = HttpContext.Session.GetInt32("ID");
            if (user_id == null)
                return RedirectToAction("Login", "Account", new
                {
                    area = "User"
                });
            ViewBag.Services = _context.Services.ToList();

            var subscriptions = _context.Users
                .Include(s => s.Subscriptions)
                .ThenInclude(s => s.Service)
                .ToList();

            return View(subscriptions);
        }
    }
}