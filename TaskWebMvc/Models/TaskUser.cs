using System.Threading.Tasks;

namespace TaskWebMvc.Models
{
    public class TaskUser
    {
        public int TaskId { get; set; }
        public Task? Task { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }

        public TaskUser(int taskId, int userId)
        {
            TaskId = taskId;
            UserId = userId;
        }

        public TaskUser() { }

        // Construtor que inicializa as propriedades de navegação
        public TaskUser(Task task, User user)
        {
            Task = task;
            TaskId = task.Id;
            User = user;
            UserId = user.Id;
        }
    }
}