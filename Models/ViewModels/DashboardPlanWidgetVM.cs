using StayFit.Models.Domain;
using System.Collections.Generic;

namespace StayFit.Models.ViewModels
{
    public class DashboardPlanWidgetVM
    {
        public List<DietPlan> ? DietPlans { get; set; }
        public List<Workout> ? Workouts { get; set; }
    }
}
