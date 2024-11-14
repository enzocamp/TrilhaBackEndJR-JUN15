public class TaskWithUsersDto
{
    public string TaskId { get; set; }
    public string TaskTitle { get; set; }
    public List<UserDto> TaskUsers { get; set; }
}
