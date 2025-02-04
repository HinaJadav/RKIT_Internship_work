using DIDemo.Models;

namespace DIDemo.BL
{
    /// <summary>Interface for task-related operations.</summary>
    public interface ITaskService
    {
        /// <summary>Creates a new task.</summary>
        Task<TaskModel> CreateTask(TaskModel task);

        /// <summary>Gets a task by ID.</summary>
        Task<TaskModel> GetTask(int id);

        /// <summary>Gets all tasks.</summary>
        Task<List<TaskModel>> GetAllTasks();

        /// <summary>Updates an existing task.</summary>
        Task<TaskModel> UpdateTask(int id, TaskModel task);

        /// <summary>Deletes a task by ID.</summary>
        Task DeleteTask(int id);

        /// <summary>Fetches all completed tasks.</summary>
        Task<List<TaskModel>> GetCompletedTasksAsync();

        /// <summary>Deletes all completed tasks.</summary>
        Task DeleteCompletedTasksAsync();
    }
}
