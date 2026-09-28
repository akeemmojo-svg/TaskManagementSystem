
namespace Milestone03_LayeredConsole.Models
{
    public class TaskItem
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Title { get; set; } = "";

        public string Description { get; set; } = "";

        public bool IsCompleted { get; set; }

        public TaskItem()
        {
        }

        public TaskItem(
            int id,
            int userId,
            string title,
            string description)
        {
            Id = id;
            UserId = userId;
            Title = title;
            Description = description;
            IsCompleted = false;
        }
    }
}