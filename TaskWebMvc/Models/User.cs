namespace TaskWebMvc.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }

        public ICollection<TaskUser> TaskUsers { get; set; } = new List<TaskUser>();

        public User(int id, string userName, string passwordHash)
        {
            Id = id;
            UserName = userName;
            PasswordHash = passwordHash;        }
    }
}
