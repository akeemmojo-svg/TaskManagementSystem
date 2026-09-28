
using Milestone03_LayeredConsole.Models;
using Milestone03_LayeredConsole.Repositories;

namespace Milestone03_LayeredConsole.Services
{
    public class UserService
    {
        private readonly JsonUserRepository repository;
        private List<User> users;

        public User? CurrentUser { get; private set; }

        public UserService(JsonUserRepository repository)
        {
            this.repository = repository;
            users = repository.LoadUsers();
        }

        public bool Register(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            if (users.Any(u => u.Username == username))
            {
                return false;
            }

            int newId = users.Count + 1;

            User newUser = new User(newId, username, password);

            users.Add(newUser);
            repository.SaveUsers(users);

            return true;
        }

        public bool Login(string username, string password)
        {
            User? user = users.FirstOrDefault(
                u => u.Username == username &&
                     u.Password == password);

            if (user == null)
            {
                return false;
            }

            CurrentUser = user;
            return true;
        }

        public void Logout()
        {
            CurrentUser = null;
        }
    }
}