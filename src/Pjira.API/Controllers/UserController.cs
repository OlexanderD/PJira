using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pjira.Core.Models;

namespace Pjira.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;

        private readonly SignInManager<IdentityUser> _signInManager;

        private readonly IMapper _mapper;

        public UserController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(User userViewModel)
        {
            
            var user = _mapper.Map<IdentityUser>(userViewModel);

            var result = await _userManager.CreateAsync(user, userViewModel.Password);

            if (!result.Succeeded)
            {
                return BadRequest("RegistationFailed");
            }
            await _signInManager.SignInAsync(user, false);           

            return Ok("Registration Completed");
        }
        [HttpGet("{Login}")]
        public async Task<IActionResult> SignIn([FromQuery] User userViewModel)
        {
           
            var user = _userManager.FindByNameAsync(userViewModel.UserName);

            if (user == null)
            {
                return BadRequest("Invalid username or password");
            }

            var result = await _signInManager.PasswordSignInAsync(userViewModel.UserName, userViewModel.Password, isPersistent: false, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                return BadRequest("Invalid username or password");
            }         

            return Ok("Login completed");
        }
        [HttpPost("Logout")]
        public async Task<IActionResult> SignOut()
        {

            if (!User.Identity.IsAuthenticated)
            {
                return BadRequest("You need to sign in first");
            }

            await _signInManager.SignOutAsync();


            return Ok("You have been signed out successfully");
        }
    }
}

