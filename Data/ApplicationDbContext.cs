using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StayFit.Models.Domain;
using StayFit.Models;

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
        public DbSet<DietPlan> DietPlans { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Progress> ProgressRecords { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DietPlan>().HasData(
    new DietPlan { Id = 1, GoalType = "WeightGain", Meal = "Breakfast", Description = "Eggs + Peanut Butter Toast" },
    new DietPlan { Id = 2, GoalType = "WeightGain", Meal = "Lunch", Description = "Chicken Wrap / Rice Bowl" },
    new DietPlan { Id = 3, GoalType = "WeightGain", Meal = "Snack", Description = "Protein Shake" },
    new DietPlan { Id = 4, GoalType = "WeightGain", Meal = "Dinner", Description = "Pasta / Potatoes / Avocado" },

    // ===== WEIGHT LOSS DIET =====
    new DietPlan { Id = 5, GoalType = "WeightLoss", Meal = "Breakfast", Description = "Oatmeal + Fruits" },
    new DietPlan { Id = 6, GoalType = "WeightLoss", Meal = "Lunch", Description = "Green Salad + Grilled Chicken" },
    new DietPlan { Id = 7, GoalType = "WeightLoss", Meal = "Dinner", Description = "Vegetable Soup" },
    new DietPlan { Id = 8, GoalType = "WeightLoss", Meal = "Snacks", Description = "Apple / Nuts" }
            );

    modelBuilder.Entity<Workout>().HasData(
        new Workout { Id = 1, Level = "WeightGain", Week = "General", Description = "Bench Press - 4 sets" },
        new Workout { Id = 2, Level = "WeightGain", Week = "General", Description = "Deadlift - 3 sets" },
        new Workout { Id = 3, Level = "WeightGain", Week = "General", Description = "Squats - 4 sets" },
        new Workout { Id = 4, Level = "WeightGain", Week = "General", Description = "Biceps + Triceps" },

        new Workout { Id = 5, Level = "WeightLoss", Week = "General", Description = "Jogging- 30 minutes" },
        new Workout { Id = 6, Level = "WeightLoss", Week = "General", Description = "HIIT - 20 minutes" },
        new Workout { Id = 7, Level = "WeightLoss", Week = "General", Description = "Cycling - 40 minutes" },
        new Workout { Id = 8, Level = "WeightLoss", Week = "General", Description = "Yoga + Stretching - 15 minutes" }
    );
        }
        


    }
}
