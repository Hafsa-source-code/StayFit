using StayFit.Data;
using StayFit.Models.Domain;
using StayFit.Models.Interfaces;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace StayFit.Models.Repositories
{
    public class PlanRepository : IPlanRepository
    {
        private readonly ApplicationDbContext _context;

        public PlanRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Plan> GetAll()
        {
            return _context.Plans.ToList();
        }

        public Plan ? GetById(int id)
        {
            using var  db = _context.Database.GetDbConnection();
    db.Open();
           return db.QueryFirstOrDefault<Plan>(
        "SELECT * FROM Plans WHERE Id = @Id",
        new { Id = id });
        }

        public void Add(Plan plan)
        {
            _context.Plans.Add(plan);
            _context.SaveChanges();
        }

        public void Update(Plan plan)
        {
            _context.Plans.Update(plan);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var plan = _context.Plans.FirstOrDefault(p => p.Id == id);
            if (plan != null)
            {
                _context.Plans.Remove(plan);
                _context.SaveChanges();
            }
        }
    }
}
