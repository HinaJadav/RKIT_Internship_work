namespace DIDemo.Models
{
    /// <summary>
    /// Represents a task with basic properties.
    /// </summary>
    public class TaskModel
    {
        /// <summary>
        /// Gets or sets the task identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the task title.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the task description.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the completion status of the task.
        /// </summary>
        public bool IsCompleted { get; set; }
    }
}
