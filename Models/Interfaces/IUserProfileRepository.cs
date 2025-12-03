using StayFit.Models.Domain;

namespace StayFit.Models.Interfaces
{
    public interface IUserProfileRepository
    {
        UserProfile ? GetByUserId(string userId);
        void Add(UserProfile profile);
        void Update(UserProfile profile);
    }
}
