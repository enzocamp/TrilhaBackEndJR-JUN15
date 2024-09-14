using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using TaskWebMvc.Interfaces;
using TaskWebMvc.Models;

namespace TaskWebMvc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IAuthService _authService;

        public AuthController(UserManager<IdentityUser> userManager, IAuthService authService)
        {
            _userManager = userManager;
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserModel model)
        {
            if (ModelState.IsValid)
            {

                if (string.IsNullOrEmpty(model.Email))
                {
                    return BadRequest("Email is necessary");
                }

                IdentityUser existingUser = await _userManager.FindByEmailAsync(model.Email);

                if (existingUser != null)
                {
                    return BadRequest("User already registered");
                }

                try
                {
                    var result = await _authService.RegisterUserAsync(model);

                    if (result.Succeeded)
                    {
                        return Ok("User created successfully");
                    }
                    else
                    {
                        return BadRequest("User creation failed");
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }


            }
            return BadRequest("Invalid model state");

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLogin model)
        {
            // Verificação das credenciais

            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(model.Email))
                {
                    return BadRequest("User email is required.");
                }

                IdentityUser existingUser = await _userManager.FindByEmailAsync(model.Email);

                if (existingUser == null)
                {
                    return BadRequest("Email adress is not registered");
                }

                bool isUserCorrect = await _userManager.CheckPasswordAsync(existingUser, model.Password);

                if (isUserCorrect)
                {
                    var token = _authService.GenerateJwtToken(existingUser);
                    return Ok(new { Token = token });
                }
                else
                {
                    return BadRequest("Invalid Password");
                }

            }
            return BadRequest("Invalid model state");
        }
    }
}