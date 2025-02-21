using DIDemo.Models;
using Newtonsoft.Json;

namespace DIDemo.BL
{
    /// <summary>Service that handles task creation, retrieval, updating, and deletion.</summary>
    public class TaskService : ITaskService
    {
        private readonly ILoggerService _loggerService;
        private readonly List<TaskModel> _tasks = new List<TaskModel>();
        private readonly string _filePath = "Data/tasks.json";

        /// <summary>
        /// 5. Constructor Injection
        /// 
        /// TaskService depends on ILoggerService
        /// </summary>
        /// <param name="loggerService"></param>
        public TaskService(ILoggerService loggerService)
        {
            _loggerService = loggerService;

            // If the file doesn't exist, create an empty JSON array
            if (!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, "[]");
            }
        }

        /// <summary>Reads tasks from a JSON file asynchronously.</summary>
        private async Task<List<TaskModel>> ReadTasksAsync()
        {
            return await Task.Run(() =>
            {
                // Read and deserialize the JSON data into a list of tasks
                string json = File.ReadAllText(_filePath);
                return JsonConvert.DeserializeObject<List<TaskModel>>(json) ?? new List<TaskModel>();
            });
        }

        /// <summary>Writes the list of tasks to a JSON file asynchronously.</summary>
        private async Task WriteTasksAsync(List<TaskModel> tasks)
        {
            await Task.Run(() =>
            {
                // Serialize the tasks list and save it to the file
                string json = JsonConvert.SerializeObject(tasks, Formatting.Indented);
                File.WriteAllText(_filePath, json);
            });
        }

        /// <summary>Creates a new task, assigns an ID, and saves it to the file.</summary>
        public async Task<TaskModel> CreateTask(TaskModel task)
        {
            // Assign the task an ID based on the current task count
            task.Id = (await ReadTasksAsync()).Count + 1;
            List<TaskModel> tasks = await ReadTasksAsync();
            tasks.Add(task);

            // Log the task creation and save it
            _loggerService.Log($"Task '{task.Title}' created.");
            await WriteTasksAsync(tasks);
            return task;
        }

        /// <summary>Gets all tasks from the file.</summary>
        public async Task<List<TaskModel>> GetAllTasks()
        {
            // Simply read and return the list of tasks
            return await ReadTasksAsync();
        }

        /// <summary>Gets a specific task by its ID.</summary>
        public async Task<TaskModel> GetTask(int id)
        {
            List<TaskModel> tasks = await ReadTasksAsync();
            // Find the task by matching the ID
            return tasks.FirstOrDefault(t => t.Id == id);
        }

        /// <summary>Updates an existing task by its ID and saves the changes.</summary>
        public async Task<TaskModel> UpdateTask(int id, TaskModel task)
        {
            List<TaskModel> tasks = await ReadTasksAsync();
            TaskModel existingTask = tasks.FirstOrDefault(t => t.Id == id);

            if (existingTask == null)
                return null;

            // Update the task properties and log the change
            existingTask.Title = task.Title;
            existingTask.Description = task.Description;
            existingTask.IsCompleted = task.IsCompleted;
            _loggerService.Log($"Task '{task.Title}' updated.");
            await WriteTasksAsync(tasks);
            return existingTask;
        }

        /// <summary>Deletes a task by its ID and updates the file.</summary>
        public async Task DeleteTask(int id)
        {
            List<TaskModel> tasks = await ReadTasksAsync();
            TaskModel task = tasks.FirstOrDefault(t => t.Id == id);

            if (task == null)
                return;

            // Remove the task and log the deletion
            tasks.Remove(task);
            _loggerService.Log($"Task '{task.Title}' deleted.");
            await WriteTasksAsync(tasks);
        }

        /// <summary>Fetches all completed tasks.</summary>
        public async Task<List<TaskModel>> GetCompletedTasksAsync()
        {
            List<TaskModel> tasks = await ReadTasksAsync();
            return tasks.Where(t => t.IsCompleted).ToList();
        }

        /// <summary>Deletes all completed tasks.</summary>
        public async Task DeleteCompletedTasksAsync()
        {
            List<TaskModel> tasks = await ReadTasksAsync();
            var completedTasks = tasks.Where(t => t.IsCompleted).ToList();

            foreach (var task in completedTasks)
            {
                tasks.Remove(task);
            }

            await WriteTasksAsync(tasks);
        }
    }
}
