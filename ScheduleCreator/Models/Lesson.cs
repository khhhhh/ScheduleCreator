using System.ComponentModel.DataAnnotations;

namespace SchedulePlan.Models
{
    public class Lesson
    {
        public int Id { get; set; }
        
        [MaxLength(30)]
        public string Name { get; set; } = string.Empty;
        public Plan Plan { get; set; }

        public int Day { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

    }
}
