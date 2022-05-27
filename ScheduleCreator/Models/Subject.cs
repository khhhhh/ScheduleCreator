using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchedulePlan.Models
{
    public class Subject
    {
        public int Id { get; set; }

        [MaxLength(30)]
        public string Name { get; set; }  = string.Empty;
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        public string UserId = String.Empty;

    }
}
