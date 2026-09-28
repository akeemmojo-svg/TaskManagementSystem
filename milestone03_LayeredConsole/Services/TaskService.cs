using Milestone03_LayeredConsole.Models;
using Milestone03_LayeredConsole.Repositories;

namespace Milestone03_LayeredConsole.Services
{
    public class TaskService
    {
        private readonly JsonTaskRepository repository;
        private List<TaskItem> tasks;

        public TaskService(JsonTaskRepository repository)
        {
            this.repository = repository;
            tasks = repository.LoadTasks();
        }

        public void AddTask(int userId, string title, string description)
        {
            int newId = tasks.Count == 0
                ? 1
                : tasks.Max(t => t.Id) + 1;

            TaskItem task = new TaskItem(
                newId,
                userId,
                title,
                description);

            tasks.Add(task);
            repository.SaveTasks(tasks);
        }

        public List<TaskItem> GetUserTasks(int userId)
        {
            return tasks
                .Where(t => t.UserId == userId)
                .ToList();
        }

        public TaskItem? FindTask(int userId, int taskId)
        {
            return tasks.FirstOrDefault(
                t => t.UserId == userId &&
                     t.Id == taskId);
        }

        public bool DeleteTask(int userId, int taskId)
        {
            TaskItem? task = FindTask(userId, taskId);

            if (task == null)
            {
                return false;
            }

            tasks.Remove(task);
            repository.SaveTasks(tasks);

            return true;
        }
    }
}