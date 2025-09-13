using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZwalks.API.Models.DTO;
using NZwalks.API.Repository.IRepository;

namespace NZwalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenRepository _tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this._userManager = userManager;
            this._tokenRepository = tokenRepository;
        }
        //POST: /api/Auth/rigister
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var IdentityUser = new IdentityUser
            {
                UserName = registerRequestDto.username,
                Email = registerRequestDto.username
            };

            var IdentityResult = await _userManager.CreateAsync(IdentityUser, registerRequestDto.password);

            if (IdentityResult.Succeeded)
            {
                // add roles to this user 
                if (string.IsNullOrEmpty(registerRequestDto.Roles) || registerRequestDto.Roles == "Reader")
                {
                    await _userManager.AddToRoleAsync(IdentityUser, "Reader");
                    return Ok("The user ceated as a Reader");
                }

                else if (registerRequestDto.Roles == "Writer")
                {
                    await _userManager.AddToRoleAsync(IdentityUser, "Writer");
                    if (IdentityResult.Succeeded)
                    {
                        return Ok("The user ceated as a writer");
                    }
                }

            }

            return BadRequest();

        }


        // POST : /api/Auth/login
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await _userManager.FindByEmailAsync(loginRequestDto.uersname);
            if (user != null)
            {
                var checkPasswordResult = await _userManager.CheckPasswordAsync(user, loginRequestDto.password);

                if (checkPasswordResult)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    // create a new token
                    if (roles != null)
                    {
                        var userJwtToken = _tokenRepository.CreateJWTToken(user, roles.ToList());

                        var response = new LoginResponseDto
                        {
                            JWTToken = userJwtToken
                        };
                        return Ok(response);

                    }
                    return BadRequest("The roles list are null");
                }
            }
            return BadRequest("The username or the password incorrect");
        }
    }
}

