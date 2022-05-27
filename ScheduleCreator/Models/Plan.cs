using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchedulePlan.Models
{
    public class Plan
    {
        public int Id { get; set; }

        [MaxLength(30)]
        public string? Name { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        public string UserId { get; set; } = String.Empty;
    }
}
