using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SubscriptionHub.Data;
using SubscriptionHub.Models;

namespace SubscriptionHub.Areas.User.Controllers
{
    [Area("User")]
    public class SubscriptionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SubscriptionController(ApplicationDbContext context)
        {
            _context = context;
        }


        public IActionResult Renew(int subscriptionId)
        {
            int user_id = HttpContext.Session.GetInt32("ID") ?? -1;
            if (user_id == -1)
                return RedirectToAction("Login", "Account");

            var sub_notracking = _context.Subscriptions.Include(s => s.Service).AsNoTracking();
                
            var sub= sub_notracking.FirstOrDefault(sub => sub.SubscriptionID == subscriptionId);
            int time= sub.Service.Duration;

            var subscription = new Subscription
            {
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(time),
                Status = "Active",
                UserID = (user_id),     
                ServiceID = sub.Service.ServiceID
            };
            subscription.SubscriptionID = subscriptionId;

            _context.Subscriptions.Update(subscription);
            _context.SaveChanges();


                return RedirectToAction("MySubscriptions", "Subscription");
        }
        public IActionResult Cancel(int subscriptionId)
        {
            _context.Subscriptions.Find(subscriptionId)!.Status= "Cancel";
                _context.SaveChanges(); 
                return RedirectToAction("MySubscriptions", "Subscription");
        }
        public IActionResult Subscribe(int serviceId)
        {
            int user_id = HttpContext.Session.GetInt32("ID")?? -1;
            if (user_id == -1)
                return RedirectToAction("Login", "Account");

            

           var servic= _context.Services.Find(serviceId);
            if(servic == null)
            return RedirectToAction("Index", "Dashboard");
                

            var subscription = new Subscription
            {
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(servic.Duration),
                Status = "Active",
                UserID = (user_id),     
                ServiceID = serviceId
            };

            _context.Subscriptions.Add(subscription);
            _context.SaveChanges();
            return RedirectToAction("Index", "Dashboard");
        }
        public IActionResult MySubscriptions(string? search, int? categoryId, string? status, decimal? minPrice)
        {
            var user_id = HttpContext.Session.GetInt32("ID");
            if (user_id == null)
                return RedirectToAction("Login", "Account");

            var user = _context.Users.Include(u => u.Subscriptions)
                .ThenInclude(s=>s.Service).ThenInclude(s=>s.Category)
                .FirstOrDefault(u => u.UserID == user_id);
            
            //var subscriptions = _context.Subscriptions
            //    .Include(s => s.Service)
            //    .ThenInclude(s => s.Category)
            //    .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                user.Subscriptions = user.Subscriptions.Where( sub=> sub.Service!.Name.Contains(search)).ToList();
             
            }

            if (categoryId.HasValue)
            {
                user.Subscriptions = user.Subscriptions.Where(s => s.Service.CategoryID == categoryId.Value).ToList();
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                user.Subscriptions = user.Subscriptions.Where(s => s.Status == status).ToList();
            }

            if (minPrice.HasValue)
            {
                user.Subscriptions = user.Subscriptions.Where(s => s.Service.Price >= minPrice.Value).ToList()  ;
            }

            ViewData["Categories"] = _context.Categories.ToList();

            return View(user.Subscriptions);
        }
    }
}