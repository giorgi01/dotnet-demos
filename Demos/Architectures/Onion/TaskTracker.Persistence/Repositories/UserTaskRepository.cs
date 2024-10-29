using Microsoft.EntityFrameworkCore;
using TaskTracker.Domain.Entities;
using TaskTracker.Domain.Interfaces;

namespace TaskTracker.Persistence.Repositories
{
    public class UserTaskRepository : IUserTaskRepository
    {
        private readonly TaskDbContext _context;

        public UserTaskRepository(TaskDbContext context)
        {
            _context = context;
        }

        public async Task<UserTask?> GetByIdAsync(int id) =>
            await _context.UserTasks.FindAsync(id);

        public async Task<IEnumerable<UserTask>> GetAllAsync() =>
            await _context.UserTasks.ToListAsync();

        public async Task<int> AddAsync(UserTask task)
        {
            await _context.UserTasks.AddAsync(task);
            await _context.SaveChangesAsync();

            return task.Id;
        }

        public async Task UpdateAsync(UserTask task)
        {
            _context.UserTasks.Update(task);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var task = await _context.UserTasks.FindAsync(id);
            if (task != null)
            {
                _context.UserTasks.Remove(task);
                await _context.SaveChangesAsync();
            }
        }
    }
}