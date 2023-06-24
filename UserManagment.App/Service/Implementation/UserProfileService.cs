using Newtonsoft.Json;
using UserManagment.App.Models;
using UserManagment.App.Models.Requests;
using UserManagment.App.Service.Interface;
using UserManagment.Data.Db.Entities;
using UserManagment.Data.Repositories.Interfaces;

namespace UserManagment.App.Service.Implementation
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IValidation _validation;

        public UserProfileService(
            IUserProfileRepository userProfileRepository,
            IValidation validation)
        {
            _userProfileRepository = userProfileRepository;
            _validation = validation;

        }

        public async Task<UserProfile> CreateUserProfile(CreateUserProfileRequest request)
        {
            UserProfileEntity userProfileEntity = new UserProfileEntity();
            userProfileEntity.UserId = request.UserId;
            userProfileEntity.FirstName = request.FirstName;
            userProfileEntity.LastName = request.LastName;
            userProfileEntity.PersonalNumber = request.PersonalNumber;

            _validation.CheckPersonalNumberFormat(request.PersonalNumber);

            await _userProfileRepository.AddAsync(userProfileEntity);
            await _userProfileRepository.SaveChangesAsync();

            var userProfile = new UserProfile
            {
                UserId = request.UserId,
                FirstName = userProfileEntity.FirstName,
                LastName = userProfileEntity.LastName,
                PersonalNumber = userProfileEntity.PersonalNumber
            };

            return userProfile;
        }

        public async Task<UserProfile> UpdateUserProfile(UpdateUserProfileRequest request)
        {
            var oldUserProfile = await _userProfileRepository.FindUser(request.OldFirstName);

            if (oldUserProfile == null)
            {
                throw new ArgumentException("User profile couldn't be found");
            }

            oldUserProfile.FirstName = request.FirstName;
            oldUserProfile.LastName = request.LastName;
            oldUserProfile.PersonalNumber = request.PersonalNumber;

            _userProfileRepository.Update(oldUserProfile);
            await _userProfileRepository.SaveChangesAsync();

            var updatedProfile = new UserProfile
            {
                UserId = request.UserId,
                FirstName = oldUserProfile.FirstName,
                LastName = oldUserProfile.LastName,
                PersonalNumber = oldUserProfile.PersonalNumber
            };

            return updatedProfile;
        }

        public async Task<List<PostEntity>> GetUserPosts(int userId)
        {
            using (var client = new HttpClient())
            {
                var postsResponse = await client.GetAsync($"https://jsonplaceholder.typicode.com/posts?userId={userId}");
                postsResponse.EnsureSuccessStatusCode();
                var postsResponseBody = await postsResponse.Content.ReadAsStringAsync();

                if (string.IsNullOrEmpty(postsResponseBody) || postsResponseBody == "[]")
                {
                    throw new Exception("Record not found");
                }

                var posts = JsonConvert.DeserializeObject<List<PostEntity>>(postsResponseBody);

                foreach (var post in posts!)
                {
                    var commentsResponse = await client.GetAsync($"https://jsonplaceholder.typicode.com/comments?postId={post.Id}");
                    commentsResponse.EnsureSuccessStatusCode();
                    var commentsResponseBody = await commentsResponse.Content.ReadAsStringAsync();

                    if (!string.IsNullOrEmpty(commentsResponseBody) && commentsResponseBody != "[]")
                    {
                        var comments = JsonConvert.DeserializeObject<List<CommentEntity>>(commentsResponseBody);
                        post.Comments = comments!;
                    }
                }

                return posts;
            }
        }

        public async Task<List<PhotoEntity>> GetUserPhotos(int userId)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"https://jsonplaceholder.typicode.com/photos?userId={userId}");
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrEmpty(responseBody) || responseBody == "[]")
                {
                    throw new Exception("Record not found");
                }

                var photos = JsonConvert.DeserializeObject<List<PhotoEntity>>(responseBody);
                return photos!;
            }
        }

        public async Task<List<TodoEntity>> GetUserTodos(int userId)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"https://jsonplaceholder.typicode.com/todos?userId={userId}");
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrEmpty(responseBody) || responseBody == "[]")
                {
                    throw new Exception("Record not found");
                }

                var todos = JsonConvert.DeserializeObject<List<TodoEntity>>(responseBody);
                return todos!;
            }
        }

        public async Task<List<CommentEntity>> GetUserComments(int userId)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"https://jsonplaceholder.typicode.com/comments?userId={userId}");
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrEmpty(responseBody) || responseBody == "[]")
                {
                    throw new Exception("Record not found");
                }

                var comments = JsonConvert.DeserializeObject<List<CommentEntity>>(responseBody);
                return comments!;
            }
        }

        public async Task<List<AlbumEntity>> GetUserAlbums(int userId)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"https://jsonplaceholder.typicode.com/albums?userId={userId}");
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrEmpty(responseBody) || responseBody == "[]")
                {
                    throw new Exception("Record not found");
                }

                var album = JsonConvert.DeserializeObject<List<AlbumEntity>>(responseBody);
                return album!;
            }
        }

        public async Task<UserProfile> GetUserProfileByUserId(int userId)
        {
            var userEntity = await _userProfileRepository.GetUserProfileByUserId(userId);

            if (userEntity == null)
            {
                throw new ArgumentException($"User with this {userId} cannot be found");
            }

            var userProfile = new UserProfile
            {
                UserId = userEntity.UserId,
                FirstName = userEntity.FirstName!,
                LastName = userEntity.LastName!,
                PersonalNumber = userEntity.PersonalNumber!
            };

            return userProfile;
        }

        public async Task RemoveUserProfile(DeleteUserProfileRequest request)
        {
            var userEntity = await _userProfileRepository.GetUserByFirstName(request.FirstName);
            if (userEntity == null)
            {
                throw new Exception($"User with {request.FirstName} does not exist.");
            }

            userEntity.IsActive = false;

            var profileEntity = userEntity.UserProfile;
            if (profileEntity != null)
            {
                _userProfileRepository.Delete(profileEntity);
            }

            await _userProfileRepository.SaveChangesAsync();
        }
    }
}
