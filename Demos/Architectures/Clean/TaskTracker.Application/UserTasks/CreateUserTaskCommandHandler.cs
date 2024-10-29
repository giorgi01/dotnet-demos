using MediatR;
using TaskTracker.Domain.Entities;
using TaskTracker.Domain.Interfaces;

namespace TaskTracker.Application.UserTasks
{
    internal sealed class CreateUserTaskCommandHandler : IRequestHandler<CreateUserTaskCommand, int>
    {
        private readonly IUserTaskRepository _repo;

        public CreateUserTaskCommandHandler(IUserTaskRepository repo)
        {
            _repo = repo;
        }

        public async Task<int> Handle(CreateUserTaskCommand request, CancellationToken cancellationToken)
        {
            var task = new UserTask
            {
                Title = request.Title,
                Description = request.Description,
                IsCompleted = request.IsCompleted,
                DueDate = request.DueDate
            };

            var taskId = await _repo.AddAsync(task);

            return taskId;
        }
    }
}
