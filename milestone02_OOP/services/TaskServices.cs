using Milestone02_OOP.Models;

namespace Milestone02_OOP.Services
{
    public class TaskService
    {
        private List<TaskItem> tasks = new List<TaskItem>();

        public void AddTask(TaskItem task)
        {
            tasks.Add(task);
        }

        public List<TaskItem> GetAllTasks()
        {
            return tasks;
        }

        public TaskItem? FindTask(int id)
        {
            return tasks.FirstOrDefault(t => t.Id == id);
        }

        public void DeleteTask(int id)
        {
            TaskItem? task = FindTask(id);

            if (task != null)
            {
                tasks.Remove(task);
            }
        }
    }
}