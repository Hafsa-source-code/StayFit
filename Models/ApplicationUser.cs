using Microsoft.AspNetCore.Identity;

namespace StayFit.Models
{
    public class ApplicationUser : IdentityUser
    {
    public string? FullName { get; set; }
    public string? Gender { get; set; }
    public int? Age { get; set; }
    public double? Height { get; set; }   // in cm on web
    public double? Weight { get; set; }   // in kg on web
    public string? FitnessGoal { get; set; } // WeightLoss / WeightGain
    }
}
