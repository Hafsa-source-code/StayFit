using StayFit.Models.Domain;

namespace StayFit.Models.Interfaces
{
    public interface IFeedbackRepository
    {
        IEnumerable<Feedback> GetAll();
        void Add(Feedback feedback);
    }
}
