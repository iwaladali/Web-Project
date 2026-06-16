using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SubscriptionHub.Models
{
    public class Subscription
    {
        [Key]
        public int SubscriptionID { get; set; }

        public DateTime  StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        [Required]
        public string Status { get; set; } = string.Empty;


        public int UserID { get; set; }
        [ForeignKey(nameof(UserID))]
        public User User { get; set; } = null!;
        
        public int ServiceID { get; set; }
        [ForeignKey(nameof(ServiceID))]
        public Service Service { get; set; } = null!;
    }
}
