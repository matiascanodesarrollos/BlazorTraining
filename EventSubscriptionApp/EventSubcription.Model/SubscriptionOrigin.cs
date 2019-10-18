using System.ComponentModel.DataAnnotations;

namespace EventSubcription.Model
{
    public class SubscriptionOrigin
    {
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
