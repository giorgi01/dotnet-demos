using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.Application.UserTasks;

namespace TaskTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ISender _sender;

        public TasksController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserTaskCommand command)
        {
            var taskId = await _sender.Send(command);

            return Ok(taskId);
        }
    }
}
