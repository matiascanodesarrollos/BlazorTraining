using System;
using System.ComponentModel.DataAnnotations;

namespace EventSubscription.Model
{
    public class Action
    {
        public int Id { get; set; }
        [Required]
        public ActionKind Kind { get; set; }
        [Required]
        public Subscription Subscription { get; set; }
        [Required]
        public DateTime? Date { get; set; }

        public override bool Equals(object obj)
        {
            return obj != null && obj is Action && this.Id == (obj as Action).Id;
        }
    }
}
