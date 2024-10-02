using System.ComponentModel.DataAnnotations;

namespace TaskWebMvc.Models
{
    public class RegisterUserModel
    {

        [Required(ErrorMessage = "UserName is necessary.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is necessary.")]
        [EmailAddress(ErrorMessage = "Wrong email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "PassWord is necessary.")]
        public string Password { get; set; }
    }
}
