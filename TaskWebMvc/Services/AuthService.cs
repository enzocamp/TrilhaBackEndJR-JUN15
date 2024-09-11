using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskWebMvc.Interfaces;
using TaskWebMvc.Models;

namespace TaskWebMvc.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public string GenerateJwtToken(IdentityUser user)
        {
            var key = _configuration["Jwt:Key"];

            if (string.IsNullOrEmpty(key))
            {
                throw new InvalidOperationException("JWT Key is not configured.");
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            if (string.IsNullOrEmpty(user.UserName))
            {
                throw new InvalidOperationException("User must have a valid UserName to generate a JWT.");
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.NameId, user.Id),
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpireMinutes"])),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<IdentityResult> RegisterUserAsync(RegisterUserModel model)
        {
            IdentityUser newUser = new IdentityUser()
            {
                Email = model.Email,
                UserName = model.UserName,
            };

            IdentityResult created = await _userManager.CreateAsync(newUser, model.Password);

            if (created.Succeeded)

                GenerateJwtToken(newUser);

            return created;
        }
    }
}
