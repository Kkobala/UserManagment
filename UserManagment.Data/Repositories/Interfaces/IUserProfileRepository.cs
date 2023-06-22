using UserManagment.Data.Db.Entities;

namespace UserManagment.Data.Repositories.Interfaces
{
    public interface IUserProfileRepository
    {
        Task AddAsync(UserProfileEntity userProfile);
        Task SaveChangesAsync();
        void Update(UserProfileEntity userProfile);
        void Delete(UserProfileEntity userProfile);
        Task<UserProfileEntity> FindUser(string firstName);
        Task<UserEntity> GetUserByFirstName(string firstName);
        Task<UserProfileEntity> GetUserProfileByUserId(int id);
    }
}
