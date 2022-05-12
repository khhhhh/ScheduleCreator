using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SchedulePlan.Models
{
    public class User : IdentityUser
    {
        [MaxLength(64)]
        public string Password { get; set; } = string.Empty;

        public List<Subject> Subjects { get; set; } = new List<Subject>();
        public List<Plan> Plans { get; set; } = new List<Plan>(); 
    }
}
