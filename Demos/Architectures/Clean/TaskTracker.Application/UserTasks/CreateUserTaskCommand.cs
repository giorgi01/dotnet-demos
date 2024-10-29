using MediatR;

namespace TaskTracker.Application.UserTasks
{
    public sealed record CreateUserTaskCommand(int Id, string Title, string Description, bool IsCompleted, DateTime DueDate) :
        IRequest<int>;
}
