using TaskTracker.Domain.Entities;

namespace TaskTracker.Domain.Interfaces
{
    public interface IUserTaskRepository
    {
        Task<UserTask?> GetByIdAsync(int id);
        Task<IEnumerable<UserTask>> GetAllAsync();
        Task<int> AddAsync(UserTask task);
        Task UpdateAsync(UserTask task);
        Task DeleteAsync(int id);
    }
}
