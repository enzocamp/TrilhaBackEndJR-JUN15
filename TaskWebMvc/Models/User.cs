using Microsoft.AspNetCore.Identity;

namespace TaskWebMvc.Models
{
    public class User : IdentityUser
    {
        public ICollection<TaskUser> TaskUsers { get; set; } = new List<TaskUser>();

        public User() : base(){}
    }
}
