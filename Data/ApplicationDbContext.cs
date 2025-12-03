using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StayFit.Models;
using StayFit.Models.Domain;

namespace StayFit.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Registering  all domain models in DbContext
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Progress> ProgressRecords { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }
    }
}
