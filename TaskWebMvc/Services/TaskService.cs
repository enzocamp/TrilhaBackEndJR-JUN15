using TaskWebMvc.Database;
using TaskWebMvc.Models;

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

    }
}
