using UserManagment.App.Models;
using UserManagment.App.Models.Requests;
using UserManagment.Data.Db.Entities;

namespace UserManagment.App.Service.Interface
{
    public interface IUserProfileService
    {
        Task<UserProfile> CreateUserProfile(CreateUserProfileRequest request);
        Task<UserProfile> UpdateUserProfile(UpdateUserProfileRequest request);
        Task<UserProfile> GetUserProfileByUserId(int userId);
        Task RemoveUserProfile(DeleteUserProfileRequest request);
        Task<List<PostEntity>> GetUserPosts(int userId);
        Task<List<PhotoEntity>> GetUserPhotos(int userId);
        Task<List<TodoEntity>> GetUserTodos(int userId);
        Task<List<CommentEntity>> GetUserComments(int userId);
        Task<List<AlbumEntity>> GetUserAlbums(int userId);
    }
}
