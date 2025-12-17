using System.ComponentModel.DataAnnotations;

namespace StayFit.Models.Domain
{
public class DietPlan
{
    public int Id { get; set; }
    public string ? GoalType { get; set; } // WeightGain / WeightLoss
    public string ? Meal { get; set; }     // Breakfast / Lunch / Dinner / Snack
    public string ? Description { get; set; }
}}