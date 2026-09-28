namespace Milestone02_OOP.Models
{
    public class TaskItem
    {
        public int Id { get; set; }

        public string Title { get; set; } = "";

        public string Description { get; set; } = "";

        public bool IsCompleted { get; set; }

        // Empty constructor
        public TaskItem()
        {
        }

        // Constructor for creating a task
        public TaskItem(int id, string title, string description)
        {
            Id = id;
            Title = title;
            Description = description;
            IsCompleted = false;
        }
    }
}