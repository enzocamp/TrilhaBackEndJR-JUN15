using System.ComponentModel.DataAnnotations;

namespace TaskWebMvc.Models
{
    public class WorkTask
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskStatus Status { get; set; }
        public virtual ICollection<TaskUser> TaskUsers { get; set; } = new List<TaskUser>();

        public WorkTask(string title, string description, TaskStatus status)
        {
            Title = title;
            Description = description;
            Status = status;
        }

    }
}
