using System.ComponentModel.DataAnnotations;

namespace TaskWebMvc.Models
{
    public class UserLogin
    {
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
    }
}
