using Dapper;
using StayFit.Models.Domain;
using System.Collections.Generic;
using StayFit.Models.Interfaces;
using StayFit.Data;

namespace StayFit.Models.Repositories
{
    public class GoalRepository : IGoalRepository
    {
        private readonly DapperContext _context;

        public GoalRepository(DapperContext context)
        {
            _context = context;
        }

        public IEnumerable<Goal> GetAll()
        {
            using var db = _context.CreateConnection();
            return db.Query<Goal>("SELECT * FROM Goals");
        }

        public Goal ? GetById(int id)
        {
            using var db = _context.CreateConnection();
            db.Open();
            return db.QueryFirstOrDefault<Goal>(
                "SELECT * FROM Goals WHERE Id = @Id",
                new { Id = id });
        }

        public void Add(Goal goal)
        {
            using var db = _context.CreateConnection();
            db.Execute("INSERT INTO Goals (Type, Description) VALUES (@Type, @Description)", goal);
        }

        public void Update(Goal goal)
        {
            using var db = _context.CreateConnection();
            db.Execute("UPDATE Goals SET Type = @Type, Description = @Description WHERE Id = @Id", goal);
        }

        public void Delete(int id)
        {
            using var db = _context.CreateConnection();
            db.Execute("DELETE FROM Goals WHERE Id = @Id", new { Id = id });
        }
    }
}
