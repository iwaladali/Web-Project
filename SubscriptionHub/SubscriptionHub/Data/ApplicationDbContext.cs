using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.EntityFrameworkCore;
using SubscriptionHub.Models;


namespace SubscriptionHub.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Service> Services => Set<Service>();
        public DbSet<Subscription> Subscriptions => Set<Subscription>();
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):
            base(options) { 
        }


    }
}
