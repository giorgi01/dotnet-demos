using TaskTracker.Application.DTOs;
using TaskTracker.Domain.Entities;
using TaskTracker.Domain.Interfaces;

namespace TaskTracker.Application.Services
{
    public class UserTaskService
    {
        private readonly IUserTaskRepository _taskRepository;

        public UserTaskService(IUserTaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<UserTaskDto?> GetTaskByIdAsync(int id)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            return task != null ? MapToDto(task) : null;
        }

        public async Task<IEnumerable<UserTaskDto>> GetAllTasksAsync()
        {
            var tasks = await _taskRepository.GetAllAsync();
            return tasks.Select(MapToDto);
        }

        public async Task<UserTaskDto?> AddTaskAsync(UserTaskDto userTaskDto)
        {
            var task = new UserTask
            {
                Title = userTaskDto.Title,
                Description = userTaskDto.Description,
                IsCompleted = userTaskDto.IsCompleted,
                DueDate = userTaskDto.DueDate
            };

            await _taskRepository.AddAsync(task);
            return MapToDto(task);
        }

        public async Task<UserTaskDto?> UpdateTaskAsync(int id, UserTaskDto userTaskDto)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null)
                return null;

            task.Title = userTaskDto.Title;
            task.Description = userTaskDto.Description;
            task.IsCompleted = userTaskDto.IsCompleted;
            task.DueDate = userTaskDto.DueDate;

            await _taskRepository.UpdateAsync(task);
            return MapToDto(task);
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null)
                return false;

            await _taskRepository.DeleteAsync(id);
            return true;
        }

        private static UserTaskDto MapToDto(UserTask task)
        {
            return new UserTaskDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                IsCompleted = task.IsCompleted,
                DueDate = task.DueDate
            };
        }
    }
}
