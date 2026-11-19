
namespace Milestone05_WebAPI.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; } = "";

        public string Password { get; set; } = "";

        public List<TaskItem> Tasks { get; set; } = new();
    }
}
