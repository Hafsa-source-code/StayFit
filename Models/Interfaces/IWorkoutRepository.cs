using StayFit.Models.Domain;

namespace StayFit.Models.Interfaces
{
    public interface IWorkoutRepository
    {
        IEnumerable<Workout> GetAll();
        Workout ? GetById(int id);
        void Add(Workout workout);
        void Update(Workout workout);
        void Delete(int id);
    }
}
