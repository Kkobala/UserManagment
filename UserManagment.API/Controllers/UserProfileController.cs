using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagment.App.Models.Requests;
using UserManagment.App.Service.Interface;
using UserManagment.Data.Db.Entities;

namespace UserManagment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileService _userProfileService;

        public UserProfileController(
            IUserProfileService userProfileService)
        {
            _userProfileService = userProfileService;
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("create-profile")]
        public async Task<IActionResult> CreateUserProfile(CreateUserProfileRequest request)
        {
            var user = await _userProfileService.CreateUserProfile(request);
            return Ok(user);
        }

        [Authorize]
        [HttpPut("update-profile")]
        public async Task<IActionResult> UpdateUserProfile(UpdateUserProfileRequest request)
        {
            var update = await _userProfileService.UpdateUserProfile(request);
            return Ok(update);
        }

        [Authorize]
        [HttpGet("view-profile")]
        public async Task<IActionResult> ViewUserProfile(int userId)
        {
            var user = await _userProfileService.GetUserProfileByUserId(userId);

            return Ok(user);
        }

        [Authorize]
        [HttpGet("get-posts")]
        public async Task<IActionResult> ViewPosts(int userId)
        {
                var userData = await _userProfileService.GetUserData(userId);
                var posts = userData.Posts;

                return Ok(posts);
        }

        [Authorize]
        [HttpGet("get-photos")]
        public async Task<IActionResult> ViewPhotos(int userId)
        {
            var userData = await _userProfileService.GetUserData(userId);
            var photo = userData.Photos;

            return Ok(photo);
        }

        [Authorize]
        [HttpGet("get-comments")]
        public async Task<IActionResult> ViewComments(int userId)
        {
            var userData = await _userProfileService.GetUserData(userId);
            var comments = userData.Comments;

            return Ok(comments);
        }

        [Authorize]
        [HttpGet("get-todos")]
        public async Task<IActionResult> ViewTodos(int userId)
        {
            var userData = await _userProfileService.GetUserData(userId);
            var todos = userData.Todos;

            return Ok(todos);
        }
        
        [Authorize]
        [HttpGet("get-albums")]
        public async Task<IActionResult> ViewAlbums(int userId)
        {
            var userData = await _userProfileService.GetUserData(userId);
            var todos = userData.Albums;

            return Ok(todos);
        }

        [Authorize]
        [HttpDelete("delete-user-profile")]
        public async Task<IActionResult> DeleteUser(DeleteUserProfileRequest request)
        {
            await _userProfileService.RemoveUserProfile(request);

            return Ok("Succesfully deleted user's profile");
        }
    }
}
