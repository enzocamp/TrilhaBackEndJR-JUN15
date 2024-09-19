using Microsoft.EntityFrameworkCore;
using TaskWebMvc.Database;
using TaskWebMvc.Models;
using TaskWebMvc.Models.DTOs;

namespace TaskWebMvc.Services
{
    public class TaskService
    {
        private readonly ApplicationDbContext _context;

        public TaskService(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<WorkTask?> FindByIdAsync(string id)
        {
            return await _context.WorkTasks.FindAsync(id);
        }

        public async Task<WorkTask> InsertAsync(WorkTask task)
        {
            _context.Add(task);
            await _context.SaveChangesAsync();

            return task;
        }

        public async Task<WorkTask> UpdateAsync(WorkTaskDto workTaskDto, string id)
        {
            var task = await FindByIdAsync(id);

            if (task == null)
            {
                throw new KeyNotFoundException($"Tarefa com ID {id} não foi encontrada");
            }

            task.Title = workTaskDto.Title;
            task.Description = workTaskDto.Description;
            task.Status = workTaskDto.Status;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception($"Error in updating task, {ex.Message}");
            }

            return task;
        }
    }
}
