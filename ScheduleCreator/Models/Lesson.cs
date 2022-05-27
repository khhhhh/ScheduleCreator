using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchedulePlan.Models
{
    public class Lesson
    {
        public int Id { get; set; }
        
        [MaxLength(30)]
        public string Name { get; set; } = string.Empty;
        [ForeignKey("PlanId")]
        public virtual Plan Plan { get; set; }
        public int PlanId { get; set; }

        public int Day { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

    }
}
