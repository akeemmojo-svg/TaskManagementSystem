using Milestone03_LayeredConsole.Repositories;
using Milestone03_LayeredConsole.Services;
using Milestone03_LayeredConsole.Models;

JsonUserRepository userRepository = new JsonUserRepository();
JsonTaskRepository taskRepository = new JsonTaskRepository();

UserService userService = new UserService(userRepository);
TaskService taskService = new TaskService(taskRepository);

bool running = true;

while (running)
{
    Console.WriteLine("\n=== TASK MANAGEMENT SYSTEM ===");

    if (userService.CurrentUser == null)
    {
        Console.WriteLine("1. Register");
        Console.WriteLine("2. Login");
        Console.WriteLine("3. Exit");
        Console.Write("Choose an option: ");

        string choice = Console.ReadLine() ?? "";

        switch (choice)
        {
            case "1":
                Console.Write("Enter username: ");
                string username = Console.ReadLine() ?? "";

                Console.Write("Enter password: ");
                string password = Console.ReadLine() ?? "";

                if (userService.Register(username, password))
                {
                    Console.WriteLine("Registration successful!");
                }
                else
                {
                    Console.WriteLine("Registration failed. Username may already exist.");
                }
                break;

            case "2":
                Console.Write("Enter username: ");
                string loginUsername = Console.ReadLine() ?? "";

                Console.Write("Enter password: ");
                string loginPassword = Console.ReadLine() ?? "";

                if (userService.Login(loginUsername, loginPassword))
                {
                    Console.WriteLine("Login successful!");
                }
                else
                {
                    Console.WriteLine("Invalid username or password.");
                }
                break;

            case "3":
                running = false;
                break;

            default:
                Console.WriteLine("Invalid option.");
                break;
        }
    }
    else
    {
        Console.WriteLine($"Logged in as: {userService.CurrentUser.Username}");
        Console.WriteLine("1. Add Task");
        Console.WriteLine("2. List My Tasks");
        Console.WriteLine("3. Find My Task");
        Console.WriteLine("4. Delete My Task");
        Console.WriteLine("5. Logout");
        Console.Write("Choose an option: ");

        string choice = Console.ReadLine() ?? "";

        switch (choice)
        {
            case "1":
                Console.Write("Enter task title: ");
                string title = Console.ReadLine() ?? "";

                Console.Write("Enter task description: ");
                string description = Console.ReadLine() ?? "";

                taskService.AddTask(
                    userService.CurrentUser.Id,
                    title,
                    description);

                Console.WriteLine("Task added successfully!");
                break;

            case "2":
                List<TaskItem> userTasks =
                    taskService.GetUserTasks(userService.CurrentUser.Id);

                if (userTasks.Count == 0)
                {
                    Console.WriteLine("You have no tasks.");
                }
                else
                {
                    foreach (TaskItem task in userTasks)
                    {
                        Console.WriteLine(
                            $"ID: {task.Id} | " +
                            $"Title: {task.Title} | " +
                            $"Description: {task.Description} | " +
                            $"Completed: {task.IsCompleted}");
                    }
                }
                break;

            case "3":
                Console.Write("Enter task ID: ");

                if (int.TryParse(Console.ReadLine(), out int searchId))
                {
                    TaskItem? foundTask =
                        taskService.FindTask(
                            userService.CurrentUser.Id,
                            searchId);

                    if (foundTask != null)
                    {
                        Console.WriteLine($"ID: {foundTask.Id}");
                        Console.WriteLine($"Title: {foundTask.Title}");
                        Console.WriteLine($"Description: {foundTask.Description}");
                        Console.WriteLine($"Completed: {foundTask.IsCompleted}");
                    }
                    else
                    {
                        Console.WriteLine("Task not found.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid ID.");
                }
                break;

            case "4":
                Console.Write("Enter task ID to delete: ");

                if (int.TryParse(Console.ReadLine(), out int deleteId))
                {
                    if (taskService.DeleteTask(
                        userService.CurrentUser.Id,
                        deleteId))
                    {
                        Console.WriteLine("Task deleted successfully!");
                    }
                    else
                    {
                        Console.WriteLine("Task not found.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid ID.");
                }
                break;

            case "5":
                userService.Logout();
                Console.WriteLine("Logged out successfully.");
                break;

            default:
                Console.WriteLine("Invalid option.");
                break;
        }
    }
}