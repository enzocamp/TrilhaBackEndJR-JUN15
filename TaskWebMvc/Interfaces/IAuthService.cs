using Microsoft.AspNetCore.Identity;
using TaskWebMvc.Models;

namespace TaskWebMvc.Interfaces
{
    public interface IAuthService
    {
        Task RegisterUserAsync(RegisterUserModel model);
        string GenerateJwtToken(IdentityUser user);

    }
}
