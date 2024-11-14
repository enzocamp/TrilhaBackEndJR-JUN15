using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<List<WorkTask>> FindAllAsync()
        {
            return await _context.WorkTasks.ToListAsync();
        }

        public async Task<WorkTask?> FindByIdAsync(string id)
        {
            return await _context.WorkTasks.FindAsync(id);
        }

        public async Task<WorkTask> InsertAsync(WorkTask task)
        {
            try
            {
                _context.Add(task);
                await _context.SaveChangesAsync();

                return task;
            }
            catch (DbUpdateException dbEx)
            {
                // Captura erros relacionados ao banco de dados, como conflitos de chave ou falhas de conexão
                throw new Exception("Error saving task to the database.", dbEx);
            }
            catch (Exception ex)
            {
                // Captura qualquer outro tipo de erro
                throw new Exception("An error occurred while saving the task.", ex);
            }
        }

        public async Task<WorkTask> UpdateAsync(WorkTaskDto workTaskDto, string id)
        {
            var task = await FindByIdAsync(id);

            if (task == null)
            {
                throw new KeyNotFoundException($"Task Id:{id} not found");
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

        public async Task<bool> DeleteAsync(string id)
        {
            var task = await FindByIdAsync(id);

            if (task == null)
            {
                return false;
            }

            try
            {
                _context.WorkTasks.Remove(task);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting task:{ex.Message}");
            }
        }

        public async Task AddUsersToTaskAsync(string taskId, List<string> usersId)
        {
            var task = await FindByIdAsync(taskId);

            if (task == null)
            {
                throw new Exception("Task not found");
            }

            foreach (var userId in usersId)
            {
                var existingAssociation = await _context.TaskUsers.AnyAsync(tu => tu.TaskId == taskId && tu.UserId == userId);

                if (!existingAssociation)
                {
                    var taskUser = new TaskUser
                    {
                        TaskId = taskId,
                        UserId = userId,
                    };
                    _context.TaskUsers.Add(taskUser);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw  new Exception($"User {userId} is already associated with the task");
                }
            }
        }

        public async Task<List<TaskWithUsersDto>> GetTasksWithUsersAsync()
        {
            var x =  await _context.WorkTasks
                .Select(task => new TaskWithUsersDto
                {
                    TaskId = task.Id,
                    TaskTitle = task.Title,
                    TaskUsers = task.TaskUsers.Select(tu => new UserDto
                    {
                        UserId = tu.UserId,
                        UserName = tu.User.UserName
                    }).ToList()
                })
                .ToListAsync();

            return x;
        }

    }
}
