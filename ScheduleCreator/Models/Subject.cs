using System.ComponentModel.DataAnnotations;

namespace SchedulePlan.Models
{
    public class Subject
    {
        public int Id { get; set; }

        [MaxLength(30)]
        public string Name { get; set; }  = string.Empty;
        public User User { get; set; }
        //public int UserId;

    }
}
