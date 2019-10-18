using System.ComponentModel.DataAnnotations;

namespace EventSubcription.Model
{
    public class ActionKind
    {
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
