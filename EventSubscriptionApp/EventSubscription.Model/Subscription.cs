using System.ComponentModel.DataAnnotations;

namespace EventSubscription.Model
{
    public class Subscription
    {
        public int Id { get; set; }
        [Required]
        public SubscriptionOrigin Origin { get; set; }
        [Required]
        public Event Event { get; set; }
        public string Info { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
