using ToDoApp.Models;

namespace ToDoApp
{
    class Program()
    {
        static void Main(string[] args)
        {
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

        static void AddToDoTask()
        {
            string title = GetStringInput("Task title: ");
            List<string> objectives = AddTaskObjectives();
            // TODO: Add functionality to add items to objectives list, loop until user exits
            return;            
        }

        // Helper methods
        // TODO: Work in progress
        static List<string> AddTaskObjectives()
        {   
            List<string> objectivesList = new List<string>([]);
            bool running = true;
            while (running)
            {
                running = false;
    
                string objective = GetStringInput("Objective: ");
                objectivesList.Add(objective);
                
                string addMore = GetStringInput("Do you want to add another one? Y/N", ["y", "n"]).ToLower();
                if (addMore.Equals("y"))
                {
                    running = true;
                }
                else if (addMore.Equals("n"))
                {
                    break;
                }
            };
            
            Console.WriteLine("Press any key to finish...");
            Console.ReadKey();
            return objectivesList;
        }
        static string GetStringInput(string prompt, List<string>? options = null)
        {
            while (true)
            {
                Console.Write(prompt);
                string result = Console.ReadLine()?.Trim() ?? string.Empty;
                
                if (!IsStringValid(result) || (options != null && !IsValidOption(result, options)))
                {
                    return result;
                }
                
                Console.WriteLine("Error: Input cannot be empty. Please try again.\n");
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
    }    
}