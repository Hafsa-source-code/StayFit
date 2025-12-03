using StayFit.Models.Domain;

namespace StayFit.Models.Interfaces
{
    public interface IGoalRepository
    {
        IEnumerable<Goal> GetAll();
        Goal ? GetById(int id);
        void Add(Goal goal);
        void Update(Goal goal);
        void Delete(int id);
    }
}
