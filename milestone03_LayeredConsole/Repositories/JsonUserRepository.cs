using System.Text.Json;
using Milestone03_LayeredConsole.Models;

namespace Milestone03_LayeredConsole.Repositories
{
    public class JsonUserRepository
    {
        private readonly string filePath = "users.json";

        public List<User> LoadUsers()
        {
            if (!File.Exists(filePath))
            {
                return new List<User>();
            }

            string json = File.ReadAllText(filePath);

            return JsonSerializer.Deserialize<List<User>>(json)
                   ?? new List<User>();
        }

        public void SaveUsers(List<User> users)
        {
            string json = JsonSerializer.Serialize(
                users,
                new JsonSerializerOptions
                {
                    WriteIndented = true
                });

            File.WriteAllText(filePath, json);
        }
    }
}