using System.ComponentModel.DataAnnotations;

namespace SchedulePlan.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;

        [MaxLength(64)]
        public string Password { get; set; } = string.Empty;

        public List<Subject> Subjects { get; set; } = new List<Subject>();
        public List<Plan> Plans { get; set; } = new List<Plan>(); 
    }
}
