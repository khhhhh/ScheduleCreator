using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SchedulePlan.Models
{
    public class User : IdentityUser
    {
        public List<Subject> Subjects { get; set; } = new List<Subject>();
        public List<Plan> Plans { get; set; } = new List<Plan>(); 
    }
}
