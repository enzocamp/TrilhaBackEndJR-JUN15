namespace TaskWebMvc.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskStatus Status { get; set; }
        public ICollection<TaskUser> TaskUsers { get; set; } = new List<TaskUser>();

        public Task(int id, string title, string description, TaskStatus status)
        {
            Id = id;
            Title = title;
            Description = description;
            Status = status;
        }
    }
}
