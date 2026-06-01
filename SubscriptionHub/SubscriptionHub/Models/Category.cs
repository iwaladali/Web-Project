using System.ComponentModel.DataAnnotations;

namespace SubscriptionHub.Models
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;


        public List<Service> Services { get; set; } = null!;
    }
}
