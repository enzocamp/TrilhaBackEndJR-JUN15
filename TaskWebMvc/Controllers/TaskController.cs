using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using TaskWebMvc.Database;
using TaskWebMvc.Models;
using TaskWebMvc.Services;
using TaskWebMvc.Models.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace TaskWebMvc.Controllers
{
    [Authorize]
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
        public async Task<IActionResult> GetAllTasks()
        {
            try
            {
                var tasks = await _taskService.FindAllAsync();

                return Ok(tasks);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetTask(string id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var task = await _taskService.FindByIdAsync(id);

                    if (task == null)
                    {
                        return NotFound($"Task id:{id} not found");
                    }

                    return Ok(task);
                }
                catch (KeyNotFoundException ex)
                {
                    return NotFound(ex.Message);
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

        [HttpPost]
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

        [HttpPut("{id}")]
        public async Task<IActionResult> EditTask([FromBody] WorkTaskDto workTaskDto, string id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var updatedTask = await _taskService.UpdateAsync(workTaskDto, id);

                    if (updatedTask == null)
                    {
                        return NotFound($"Task Id:{id} not found.");
                    }

                    return Ok(updatedTask);

                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Server error: {ex.Message}");
                }
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(string id)
        {
            try
            {
                var result = await _taskService.DeleteAsync(id);

                if (!result)
                {
                    return NotFound($"Task id:{id} not found");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Server error:{ex.Message}");
            }
        }

        [HttpPost("assign-users")]
        public async Task<IActionResult> AssignUsersToTask([FromBody] TaskUserDto taskUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _taskService.AddUsersToTaskAsync(taskUserDto.TaskId, taskUserDto.UserIds);
                return Ok("Users assigned to task successfully");
            }
            catch (KeyNotFoundException ex)
            {
                // Exceção específica para quando a tarefa ou usuários não são encontrados
                return NotFound($"Resource not found: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

    }
}