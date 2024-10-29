using Microsoft.AspNetCore.Mvc;
using TaskTracker.Application.DTOs;
using TaskTracker.Application.Services;

namespace TaskTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly UserTaskService _taskService;

        public TasksController(UserTaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            return Ok(task);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tasks = await _taskService.GetAllTasksAsync();
            return Ok(tasks);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserTaskDto taskDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdTask = await _taskService.AddTaskAsync(taskDto);
            if (createdTask != null) return Ok(createdTask.Id);
            return BadRequest();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UserTaskDto taskDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedTask = await _taskService.UpdateTaskAsync(id, taskDto);
            return updatedTask != null ? Ok(updatedTask) : NotFound();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _taskService.DeleteTaskAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
