using Microsoft.EntityFrameworkCore;
using SchedulePlan.Models;

namespace SchedulePlan.DataAccess
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions options) : base(options) { }

        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
