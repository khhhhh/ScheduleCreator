using System.ComponentModel.DataAnnotations;

namespace SchedulePlan.Models
{
    public class Plan
    {
        public int Id { get; set; }

        [MaxLength(30)]
        public string? Name { get; set; } 
        public User User { get; set; }
        //public int UserId { get; set; }
    }
}
