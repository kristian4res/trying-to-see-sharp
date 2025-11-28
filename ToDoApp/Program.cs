using ToDoApp.Models;
using ToDoApp.Services;

namespace ToDoApp
{
    class Program()
    {
        static void Main(string[] args)
        {
            var dataService = new DataService();
            var taskManagerService = new TaskManagerService(dataService);
            bool running = true;

            Console.Clear();
            Console.WriteLine("===========================================");
            Console.WriteLine("  To Do App");
            Console.WriteLine("===========================================");

            while (running)
            {
                ShowMenu();

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddToDoTask(taskManagerService);
                        break;
                    case "5":
                        running = false;
                        Console.WriteLine("Exiting...");
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.\n");
                        break;
                }
            }
        }

        static void ShowMenu()
        {
            Console.WriteLine("1. Add new task\n");
            Console.WriteLine("2. View task\n");
            Console.WriteLine("3. Update task\n");
            Console.WriteLine("4. Delete task\n");
            Console.WriteLine("5. Exit\n");
        }

        static void AddToDoTask(TaskManagerService taskManagerService)
        {
            string title = GetStringInput("Task title: ");
            List<string> objectives = AddTaskObjectives();
            
            if (GetConfirmation("Confirm add task?"))
            {
                taskManagerService.AddTask(title, objectives);
            }
            else
            {
                Console.WriteLine("Cancelled task creation.\n");
            }
        }

        // Helper methods
        static List<string> AddTaskObjectives()
        {   
            List<string> objectives = new();  // Simpler syntax
            
            while (true)
            {
                string objective = GetStringInput("Objective: ");
                objectives.Add(objective);
                
                string addMore = GetStringInput("Add another? (y/n): ", new List<string> { "y", "n" }).ToLower();
                
                if (addMore == "n")
                {
                    break;
                }
            }
            
            return objectives;
        }
        static string GetStringInput(string prompt, List<string>? options = null)
        {
            while (true)
            {
                Console.Write(prompt);
                string result = Console.ReadLine()?.Trim() ?? string.Empty;
                
                if (string.IsNullOrWhiteSpace(result))
                {
                    Console.WriteLine("Error: Input cannot be empty. Please try again.\n");
                    continue;
                }
                
                if (options != null && !options.Contains(result))
                {
                    Console.WriteLine($"Error: Invalid option. Choose from: {string.Join(", ", options)}\n");
                    continue;
                }
                
                return result;
            }
        }
        static bool IsValidOption(string? input, List<string> options) 
        {
            return input != null && options.Contains(input);
        }

        static bool IsStringValid(string? input)
        {
            return !string.IsNullOrEmpty(input) || !string.IsNullOrEmpty(input);
        }

        static bool GetConfirmation(string message)
        {
            while (true)
            {
                Console.Write($"{message} (y/n): ");
                string? input = Console.ReadLine()?.Trim().ToLower();

                if (input == "y" || input == "yes") return true;
                if (input == "n" || input == "no") return false;
                
                Console.WriteLine("Please enter y or n.");
            }
        }
    }    
}