using System.Text.Json;
using Milestone02_OOP.Models;

namespace Milestone02_OOP.Repositories
{
    public class JsonTaskRepository
    {
        private readonly string filePath = "tasks.json";

        public List<TaskItem> LoadTasks()
        {
            if (!File.Exists(filePath))
            {
                return new List<TaskItem>();
            }

            string json = File.ReadAllText(filePath);

            return JsonSerializer.Deserialize<List<TaskItem>>(json)
                   ?? new List<TaskItem>();
        }

        public void SaveTasks(List<TaskItem> tasks)
        {
            string json = JsonSerializer.Serialize(
                tasks,
                new JsonSerializerOptions
                {
                    WriteIndented = true
                });

            File.WriteAllText(filePath, json);
        }
    }
}