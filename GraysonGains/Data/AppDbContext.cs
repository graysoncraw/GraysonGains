using Microsoft.EntityFrameworkCore;
using GraysonGains.Models.Entities;

namespace GraysonGains.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        { 
            
        }
        public DbSet<Users> Users { get; set; }
        public DbSet<UserPRs> UserPRs { get; set; }
        public DbSet<UserWorkoutDays> UserWorkoutDays { get; set; }
        public DbSet<WorkoutLogs> WorkoutLogs { get; set; }
    }
}
