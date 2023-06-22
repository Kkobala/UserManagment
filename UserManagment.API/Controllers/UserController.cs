using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserManagment.App.Auth;
using UserManagment.App.Models.AuthRequest;
using UserManagment.App.Service.Interface;
using UserManagment.Data.Db;
using UserManagment.Data.Db.Entities;

namespace UserManagment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly TokenGenerator _tokenGenerator;
        private readonly UserManager<UserEntity> _userManager;
        private readonly AppDbContext _db;
        private readonly SignInManager<UserEntity> _signInManager;

        public UserController(
            TokenGenerator tokenGenerator,
            UserManager<UserEntity> userManager,
            AppDbContext db,
            SignInManager<UserEntity> signInManager)
        {
            _tokenGenerator = tokenGenerator;
            _userManager = userManager;
            _db = db;
            _signInManager = signInManager;

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new UserEntity
            {
                UserName = request.Email,
                Email = request.Email,
                Password = request.Password,
                IsActive = true
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            await _db.SaveChangesAsync();

            if (result.Succeeded)
            {
                return Ok(request);
            }

            return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return BadRequest("Invalid email or password");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var token = _tokenGenerator.Generate(user);
                return Ok(new { Token = token });
            }

            return BadRequest("Invalid email or password");
        }
    }
}

