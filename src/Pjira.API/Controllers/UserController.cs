using AutoMapper;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using Pjira.Application.DtoModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Pjira.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;

        private readonly SignInManager<IdentityUser> _signInManager;

        private readonly IMapper _mapper;

        private readonly IConfiguration _config;

        public UserController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IMapper mapper, IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _config = config;
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(User userViewModel)
        {
            
            var user = _mapper.Map<IdentityUser>(userViewModel);

            var result = await _userManager.CreateAsync(user, userViewModel.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            var token = GenerateJwtToken(user);
            return Ok(new { token });
        }
        [HttpGet("Login")]
        public async Task<IActionResult> SignIn([FromQuery] User userViewModel)
        {
           
            var user = await _userManager.FindByNameAsync(userViewModel.UserName);

            if (user == null)
            {
                return BadRequest("Invalid username or password");
            }

            if (!await _userManager.CheckPasswordAsync(user, userViewModel.Password))
            {
                return Unauthorized("Invalid username or password");
            }

            var token = GenerateJwtToken(user);
            return Ok(new { token });
        }
        [HttpPost("Logout")]
        [Authorize]
        public async Task<IActionResult> SignOut()
        {

            if (!User.Identity.IsAuthenticated)
            {
                return BadRequest("You need to sign in first");
            }

            await _signInManager.SignOutAsync();


            return Ok("You have been signed out successfully");
        }

        private string GenerateJwtToken(IdentityUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),

                new (ClaimTypes.Name, user.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                 issuer: _config["Jwt:Issuer"],
                 audience: _config["Jwt:Audience"],
                 claims: claims,
                 expires: DateTime.Now.AddHours(3),
                  signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

