using DIDemo.BL;
using DIDemo.Models;
using Microsoft.AspNetCore.Mvc;

namespace DIDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        /// <summary>Gets all tasks.</summary>
        [HttpGet]
        public async Task<IActionResult> GetAllTasks()
        {
            List<TaskModel> tasks = await _taskService.GetAllTasks();
            return Ok(tasks);
        }

        /// <summary>Gets a task by its ID.</summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTask(int id)
        {
            TaskModel task = await _taskService.GetTask(id);
            return task == null ? NotFound() : Ok(task);
        }

        /// <summary>Creates a new task.</summary>
        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] TaskModel task)
        {
            TaskModel createdTask = await _taskService.CreateTask(task);
            return Ok(createdTask);
        }

        /// <summary>Updates an existing task by its ID.</summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] TaskModel task)
        {
            TaskModel updatedTask = await _taskService.UpdateTask(id, task);
            return updatedTask == null ? NotFound() : Ok(updatedTask);
        }

        /// <summary>Deletes a task by its ID.</summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            await _taskService.DeleteTask(id);
            return NoContent();
        }

        /// <summary>Gets or deletes all completed tasks.</summary>
        /// <param name="delete">If true, deletes all completed tasks; otherwise, fetches them.</param>
        [HttpGet("completed")]
        [HttpDelete("completed")]
        public async Task<IActionResult> GetOrDeleteCompletedTasks(bool delete = false)
        {
            if (delete)
            {
                // Delete completed tasks
                await _taskService.DeleteCompletedTasksAsync();
                return NoContent(); // Successfully deleted
            }
            else
            {
                // Fetch completed tasks
                List<TaskModel> completedTasks = await _taskService.GetCompletedTasksAsync();
                return Ok(completedTasks); // Return the list of completed tasks
            }
        }
    }
}
