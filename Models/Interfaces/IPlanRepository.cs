using StayFit.Models.Domain;

namespace StayFit.Models.Interfaces
{
    public interface IPlanRepository
    {
        IEnumerable<Plan> GetAll();
        Plan ? GetById(int id);
        void Add(Plan plan);
        void Update(Plan plan);
        void Delete(int id);
    }
}
