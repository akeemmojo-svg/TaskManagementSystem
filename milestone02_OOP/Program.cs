using Milestone02_OOP.Models;
using Milestone02_OOP.Repositories;
using Milestone02_OOP.Services;

JsonTaskRepository repository = new JsonTaskRepository();
TaskService taskService = new TaskService();

List<TaskItem> savedTasks = repository.LoadTasks();

foreach (TaskItem task in savedTasks)
{
    taskService.AddTask(task);
}

bool running = true;

while (running)
{
    Console.WriteLine("\n=== TASK MANAGEMENT SYSTEM ===");
    Console.WriteLine("1. Add Task");
    Console.WriteLine("2. List Tasks");
    Console.WriteLine("3. Find Task");
    Console.WriteLine("4. Delete Task");
    Console.WriteLine("5. Exit");
    Console.Write("Choose an option: ");

    string choice = Console.ReadLine() ?? "";

    switch (choice)
    {
        case "1":
            Console.Write("Enter task title: ");
            string title = Console.ReadLine() ?? "";

            Console.Write("Enter task description: ");
            string description = Console.ReadLine() ?? "";

            int newId = taskService.GetAllTasks().Count + 1;

            TaskItem newTask = new TaskItem(newId, title, description);

            taskService.AddTask(newTask);
            repository.SaveTasks(taskService.GetAllTasks());

            Console.WriteLine("Task added successfully!");
            break;

        case "2":
            List<TaskItem> tasks = taskService.GetAllTasks();

            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks found.");
            }
            else
            {
                foreach (TaskItem task in tasks)
                {
                    Console.WriteLine(
                        $"ID: {task.Id} | Title: {task.Title} | Description: {task.Description} | Completed: {task.IsCompleted}"
                    );
                }
            }
            break;

        case "3":
            Console.Write("Enter task ID: ");

            if (int.TryParse(Console.ReadLine(), out int searchId))
            {
                TaskItem? foundTask = taskService.FindTask(searchId);

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
                TaskItem? taskToDelete = taskService.FindTask(deleteId);

                if (taskToDelete != null)
                {
                    taskService.DeleteTask(deleteId);
                    repository.SaveTasks(taskService.GetAllTasks());

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
            repository.SaveTasks(taskService.GetAllTasks());
            running = false;
            Console.WriteLine("Goodbye!");
            break;

        default:
            Console.WriteLine("Invalid option. Please try again.");
            break;
    }
}