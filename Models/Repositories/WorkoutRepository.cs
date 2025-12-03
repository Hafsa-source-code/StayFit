using Dapper;
using Microsoft.EntityFrameworkCore;
using StayFit.Data;
using StayFit.Models.Domain;
using StayFit.Models.Interfaces;
using System.Data;

namespace StayFit.Models.Repositories
{
    public class WorkoutRepository : IWorkoutRepository
    {
        private readonly ApplicationDbContext _context;

        public WorkoutRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Create a database connection
        private IDbConnection CreateConnection() => _context.Database.GetDbConnection();

        // Get all workouts
        public IEnumerable<Workout> GetAll()
        {
            using var db = CreateConnection();
            db.Open();
            return db.Query<Workout>("SELECT * FROM Workouts");
        }

        // Get workout by Id
        public Workout ? GetById(int id)
        {
            using var db = CreateConnection();
            db.Open();
            return db.QueryFirstOrDefault<Workout>(
                "SELECT * FROM Workouts WHERE Id=@Id",
                new { Id = id });
        }

        // Add a new workout
        public void Add(Workout workout)
        {
            using var db = CreateConnection();
            db.Open();
            var sql = @"INSERT INTO Workouts (Level, Description) 
                        VALUES (@Level, @Description)";
            db.Execute(sql, workout);
        }

        // Update an existing workout
        public void Update(Workout workout)
        {
            using var db = CreateConnection();
            db.Open();
            var sql = @"UPDATE Workouts 
                        SET Level=@Level, Description=@Description 
                        WHERE Id=@Id";
            db.Execute(sql, workout);
        }

        // Delete a workout by Id
        public void Delete(int id)
        {
            using var db = CreateConnection();
            db.Open();
            db.Execute("DELETE FROM Workouts WHERE Id=@Id", new { Id = id });
        }
    }
}
