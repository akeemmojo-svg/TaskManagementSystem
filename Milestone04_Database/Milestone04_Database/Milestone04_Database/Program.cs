
using Microsoft.EntityFrameworkCore;
using Milestone04_Database.Data;
using Milestone04_Database.Models;
using Milestone04_Database.Services;

// Create database and tables
using (var db = new AppDbContext())
{
    db.Database.EnsureCreated();
}

TaskService service = new TaskService();

bool running = true;

while (running)
{
    Console.WriteLine("\n=== TASK MANAGEMENT SYSTEM ===");
    Console.WriteLine("1. Register");
    Console.WriteLine("2. Login");
    Console.WriteLine("3. Exit");
    Console.Write("Choose: ");

    string choice = Console.ReadLine() ?? "";

    if (choice == "1")
    {
        Console.Write("Username: ");
        string username = Console.ReadLine() ?? "";

        Console.Write("Password: ");
        string password = Console.ReadLine() ?? "";

        service.AddUser(username, password);
        Console.WriteLine("User registered!");
    }
    else if (choice == "2")
    {
        Console.Write("Username: ");
        string username = Console.ReadLine() ?? "";

        Console.Write("Password: ");
        string password = Console.ReadLine() ?? "";

        using var db = new AppDbContext();

        User? user = db.Users.FirstOrDefault(
            u => u.Username == username &&
                 u.Password == password);

        if (user == null)
        {
            Console.WriteLine("Invalid login.");
            continue;
        }

        Console.WriteLine($"Welcome {user.Username}!");

        bool loggedIn = true;

        while (loggedIn)
        {
            Console.WriteLine("\n1. Add Task");
            Console.WriteLine("2. List My Tasks");
            Console.WriteLine("3. Delete Task");
            Console.WriteLine("4. Logout");
            Console.Write("Choose: ");

            string option = Console.ReadLine() ?? "";

            if (option == "1")
            {
                Console.Write("Title: ");
                string title = Console.ReadLine() ?? "";

                Console.Write("Description: ");
                string description = Console.ReadLine() ?? "";

                service.AddTask(user.Id, title, description);
                Console.WriteLine("Task added!");
            }
            else if (option == "2")
            {
                List<TaskItem> tasks = service.GetTasks(user.Id);

                foreach (TaskItem task in tasks)
                {
                    Console.WriteLine(
                        $"{task.Id} | {task.Title} | {task.Description}");
                }

                if (tasks.Count == 0)
                    Console.WriteLine("You have no tasks.");
            }
            else if (option == "3")
            {
                Console.Write("Enter task ID: ");

                if (int.TryParse(Console.ReadLine(), out int id))
                {
                    service.DeleteTask(id);
                    Console.WriteLine("Delete operation completed.");
                }
            }
            else if (option == "4")
            {
                loggedIn = false;
            }
        }
    }
    else if (choice == "3")
    {
        running = false;
    }
}
