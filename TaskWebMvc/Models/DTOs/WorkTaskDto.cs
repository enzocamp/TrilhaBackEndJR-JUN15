using System.ComponentModel.DataAnnotations;

namespace TaskWebMvc.Models.DTOs
{
    public class WorkTaskDto
    {
        [Required(ErrorMessage = "The Title is required.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "The Description is required.")]
        public string Description { get; set; }

        [Range(1,4, ErrorMessage = "Invalid Status value it must be bbetween 1 and 4")]
        public TaskStatus Status { get; set; }
    }
}
