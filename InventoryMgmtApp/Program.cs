using InventoryMgmtApp.Services;

namespace InventoryMgmtApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var dataService = new DataService();
            var inventoryService = new InventoryService(dataService);
            bool running = true;

            Console.Clear();
            Console.WriteLine("===========================================");
            Console.WriteLine("  Inventory Management System");
            Console.WriteLine("===========================================");
            Console.WriteLine($"Loaded {inventoryService.GetProductCount()} products\n");

            while (running)
            {
                ShowMenu();
                string? choice = Console.ReadLine();

                Console.WriteLine();

                switch (choice)
                {
                    case "1": 
                        AddNewProduct(inventoryService); 
                        break;
                    case "2": 
                        SellProduct(inventoryService); 
                        break;
                    case "3": 
                        RestockProduct(inventoryService); 
                        break;
                    case "4": 
                        inventoryService.ViewAllProducts(); 
                        break;
                    case "5": 
                        RemoveProduct(inventoryService); 
                        break;
                    case "6":
                        running = false;
                        Console.WriteLine("Exiting...\n");
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }

                if (running)
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        static void ShowMenu()
        {
            Console.WriteLine("\n--- Main Menu ---");
            Console.WriteLine("1. Add New Product");
            Console.WriteLine("2. Sell Product");
            Console.WriteLine("3. Restock Product");
            Console.WriteLine("4. View All Products");
            Console.WriteLine("5. Remove Product");
            Console.WriteLine("6. Exit");
            Console.Write("\nEnter your choice: ");
        }

        static void AddNewProduct(InventoryService service)
        {
            Console.WriteLine("\n--- Add New Product ---\n");

            string name = GetStringInput("Product Name: ");
            decimal price = GetDecimalInput("Price: ");
            int quantity = GetIntInput("Quantity: ");

            service.DisplayProductDetails(name, price, quantity);
            
            if (GetConfirmation("Confirm add product?"))
            {
                service.AddProduct(name, price, quantity);
            }
            else
            {
                Console.WriteLine("Cancelled.");
            }
        }

        static void SellProduct(InventoryService service)
        {
            Console.WriteLine("\n--- Sell Product ---\n");

            int id = GetIntInput("Product ID: ");
            int quantity = GetIntInput("Quantity: ");

            service.SellProduct(id, quantity);
        }

        static void RestockProduct(InventoryService service)
        {
            Console.WriteLine("\n--- Restock Product ---\n");

            int id = GetIntInput("Product ID: ");
            int quantity = GetIntInput("Quantity to add: ");

            service.RestockProduct(id, quantity);
        }

        static void RemoveProduct(InventoryService service)
        {
            Console.WriteLine("\n--- Remove Product ---\n");
            service.ViewAllProducts();
            
            int id = GetIntInput("\nProduct ID: ");

            var product = service.GetProductById(id);
            if (product == null)
            {
                Console.WriteLine($"Error: Product ID {id} not found.");
                return;
            }

            Console.WriteLine($"\n{product}");

            if (GetConfirmation("\nDelete this product?"))
            {
                service.RemoveProduct(id);
            }
            else
            {
                Console.WriteLine("Cancelled.");
            }
        }

        // Helper methods for user input
        static string GetStringInput(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string result = Console.ReadLine()?.Trim() ?? string.Empty;
                
                if (!string.IsNullOrWhiteSpace(result))
                {
                    return result;
                }
                
                Console.WriteLine("Error: Input cannot be empty. Please try again.\n");
            }
        }

        static decimal GetDecimalInput(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (decimal.TryParse(Console.ReadLine(), out decimal result) && result >= 0)
                {
                    return result;
                }
                
                Console.WriteLine("Error: Please enter a valid number.\n");
            }
        }

        static int GetIntInput(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out int result) && result >= 0)
                {
                    return result;
                }
                
                Console.WriteLine("Error: Please enter a valid number.\n");
            }
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