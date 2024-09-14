using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using TaskWebMvc.Database;
using TaskWebMvc.Models;
using TaskWebMvc.Services;
using TaskWebMvc.Models.DTOs;

namespace TaskWebMvc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly TaskService _taskService;

        public TaskController(TaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<ActionResult> Create([FromBody] WorkTaskDto taskDto)
        {
            if (ModelState.IsValid)
            {
                var task = new WorkTask(taskDto.Title, taskDto.Description, taskDto.Status); 

                try
                {
                    await _taskService.InsertAsync(task);

                    return CreatedAtAction("GetTask", new { id = task.Id }, task);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
