using System;
using System.ComponentModel.DataAnnotations;

namespace EventSubscription.Model
{
    public class Event
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        public DateTime? Date { get; set; }

        public override bool Equals(object obj)
        {
            return obj != null && obj is Event && this.Id == (obj as Event).Id;
        }
    }
}
