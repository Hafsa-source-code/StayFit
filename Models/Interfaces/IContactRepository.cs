using StayFit.Models.Domain;

namespace StayFit.Models.Interfaces
{
    public interface IContactRepository
    {
        IEnumerable<ContactMessage> GetAll();
        void Add(ContactMessage message);
    }
}
