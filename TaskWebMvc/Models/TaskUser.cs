using System.Threading.Tasks;

namespace TaskWebMvc.Models
{
    public class TaskUser
    {
        public string TaskId { get; set; }
        public WorkTask? Task { get; set; }

        public string UserId { get; set; }
        public User? User { get; set; }

        public TaskUser(string taskId, string userId)
        {
            TaskId = taskId;
            UserId = userId;
        }

        public TaskUser() { }

        // Construtor que inicializa as propriedades de navegação
        public TaskUser(WorkTask task, User user)
        {
            Task = task;
            TaskId = task.Id;
            User = user;
            UserId = user.Id;
        }
    }
}