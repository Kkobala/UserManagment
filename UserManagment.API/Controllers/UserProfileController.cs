using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagment.App.Models.Requests;
using UserManagment.App.Service.Interface;

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

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPut("update-profile")]
        public async Task<IActionResult> UpdateUserProfile(UpdateUserProfileRequest request)
        {
            var update = await _userProfileService.UpdateUserProfile(request);
            return Ok(update);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("view-profile")]
        public async Task<IActionResult> ViewUserProfile(int userId)
        {
            var user = await _userProfileService.GetUserProfileByUserId(userId);
            return Ok(user);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("get-posts")]
        public async Task<IActionResult> ViewPosts(int userId)
        {
            var userData = await _userProfileService.GetUserPosts(userId);
            return Ok(userData);
        }

        [Authorize]
        [HttpGet("get-photos")]
        public async Task<IActionResult> ViewPhotos(int userId)
        {
            var userData = await _userProfileService.GetUserPhotos(userId);
            return Ok(userData);
        }


        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("get-comments")]
        public async Task<IActionResult> ViewComments(int userId)
        {
            var userData = await _userProfileService.GetUserComments(userId);
            return Ok(userData);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("get-todos")]
        public async Task<IActionResult> ViewTodos(int userId)
        {
            var userData = await _userProfileService.GetUserTodos(userId);
            return Ok(userData);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("get-albums")]
        public async Task<IActionResult> ViewAlbums(int userId)
        {
            var userData = await _userProfileService.GetUserAlbums(userId);
            return Ok(userData);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpDelete("delete-user-profile")]
        public async Task<IActionResult> DeleteUser(DeleteUserProfileRequest request)
        {
            await _userProfileService.RemoveUserProfile(request);

            return Ok("Succesfully deleted user's profile");
        }
    }
}
