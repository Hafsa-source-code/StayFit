using StayFit.Models.Domain;

namespace StayFit.Models.Interfaces
{
    public interface IProgressRepository
    {
        IEnumerable<Progress> GetUserProgress(string userId);
        void Add(Progress progress);
    }
}
