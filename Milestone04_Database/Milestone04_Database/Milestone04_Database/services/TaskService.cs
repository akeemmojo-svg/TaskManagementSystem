using Microsoft.EntityFrameworkCore;
using Milestone04_Database.Data;
using Milestone04_Database.Models;

namespace Milestone04_Database.Services
{
    public class TaskService
    {
        public void AddUser(string username, string password)
        {
            using var db = new AppDbContext();

            User user = new User
            {
                Username = username,
                Password = password
            };

            db.Users.Add(user);
            db.SaveChanges();
        }

        public void AddTask(int userId, string title, string description)
        {
            using var db = new AppDbContext();

            TaskItem task = new TaskItem
            {
                UserId = userId,
                Title = title,
                Description = description,
                IsCompleted = false
            };

            db.Tasks.Add(task);
            db.SaveChanges();
        }

        public List<TaskItem> GetTasks(int userId)
        {
            using var db = new AppDbContext();

            return db.Tasks
                .Where(t => t.UserId == userId)
                .ToList();
        }

        public void DeleteTask(int taskId)
        {
            using var db = new AppDbContext();

            TaskItem? task = db.Tasks.Find(taskId);

            if (task != null)
            {
                db.Tasks.Remove(task);
                db.SaveChanges();
            }
        }
    }
}