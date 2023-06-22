using Microsoft.EntityFrameworkCore;
using UserManagment.Data.Db;
using UserManagment.Data.Db.Entities;
using UserManagment.Data.Repositories.Interfaces;

namespace UserManagment.Data.Repositories.Implementations
{
    public class UserProfileRepository: IUserProfileRepository
    {
        private readonly AppDbContext _db;

        public UserProfileRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(UserProfileEntity userProfile)
        {
            await _db.UserProfiles.AddAsync(userProfile);
        }

        public void Update(UserProfileEntity userProfile)
        {
            _db.UserProfiles.Update(userProfile);
        }

        public void Delete(UserProfileEntity userProfile)
        {
            _db.UserProfiles.Remove(userProfile);
        }

        public async Task<UserProfileEntity> FindUser(string firstName)
        {
            var userProfile = await _db.UserProfiles.FirstOrDefaultAsync(x => x.FirstName == firstName);

            if (userProfile == null)
            {
                throw new ArgumentException("User profile couldn't be found");
            }

            return userProfile;
        }

        public async Task<UserEntity> GetUserByFirstName(string firstName)
        {
            var user = await _db.Users.Include(u => u.UserProfile)
                          .FirstOrDefaultAsync(u => u.UserProfile.FirstName == firstName);

            if (user == null)
                throw new ArgumentException("User doesn't exist!");

            return user;
        }

        public async Task<UserProfileEntity> GetUserProfileByUserId(int id)
        {
            var userId = await _db.UserProfiles.FirstOrDefaultAsync(u => u.UserId == id);

            if (userId == null)
                throw new ArgumentException("Cannot find user");

            return userId;
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
