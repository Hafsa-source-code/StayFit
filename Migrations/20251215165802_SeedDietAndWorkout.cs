using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StayFit.Migrations
{
    /// <inheritdoc />
    public partial class SeedDietAndWorkout : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "DietPlans",
                columns: new[] { "Id", "Description", "GoalType", "Meal" },
                values: new object[,]
                {
                    { 1, "Eggs + Peanut Butter Toast", "WeightGain", "Breakfast" },
                    { 2, "Chicken Wrap / Rice Bowl", "WeightGain", "Lunch" },
                    { 3, "Protein Shake", "WeightGain", "Snack" },
                    { 4, "Pasta / Potatoes / Avocado", "WeightGain", "Dinner" },
                    { 5, "Oatmeal + Fruits", "WeightLoss", "Breakfast" },
                    { 6, "Green Salad + Grilled Chicken", "WeightLoss", "Lunch" },
                    { 7, "Vegetable Soup", "WeightLoss", "Dinner" },
                    { 8, "Apple / Nuts", "WeightLoss", "Snacks" }
                });

            migrationBuilder.InsertData(
                table: "Workouts",
                columns: new[] { "Id", "Description", "Level", "Week" },
                values: new object[,]
                {
                    { 1, "Bench Press – 4 sets", "WeightGain", "General" },
                    { 2, "Deadlift – 3 sets", "WeightGain", "General" },
                    { 3, "Squats – 4 sets", "WeightGain", "General" },
                    { 4, "Biceps + Triceps", "WeightGain", "General" },
                    { 5, "Jogging – 30 minutes", "WeightLoss", "General" },
                    { 6, "HIIT – 20 minutes", "WeightLoss", "General" },
                    { 7, "Cycling – 40 minutes", "WeightLoss", "General" },
                    { 8, "Yoga + Stretching – 15 minutes", "WeightLoss", "General" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DietPlans",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DietPlans",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "DietPlans",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "DietPlans",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "DietPlans",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "DietPlans",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "DietPlans",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "DietPlans",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Workouts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Workouts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Workouts",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Workouts",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Workouts",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Workouts",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Workouts",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Workouts",
                keyColumn: "Id",
                keyValue: 8);
        }
    }
}
