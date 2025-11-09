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
            Console.WriteLine("╔════════════════════════════════════════════╗");
            Console.WriteLine("║     INVENTORY MANAGEMENT SYSTEM v1.0       ║");
            Console.WriteLine("╚════════════════════════════════════════════╝");
            Console.WriteLine($"\n✓ Data loaded successfully ({inventoryService.GetProductCount()} products)\n");

            while (running)
            {
                ShowMenu();
                string? choice = Console.ReadLine();

                Console.WriteLine();

                switch (choice)
                {
                    case "1": AddNewProduct(inventoryService); break;
                    case "2": SellProduct(inventoryService); break;
                    case "3": RestockProduct(inventoryService); break;
                    case "4": inventoryService.ViewAllProducts(); break;
                    case "5": RemoveProduct(inventoryService); break;
                    case "6":
                        running = false;
                        Console.WriteLine("👋 Closing Inventory Management System...\n");
                        break;
                    default:
                        Console.WriteLine("❌ Invalid option. Please select 1-6.");
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
            Console.WriteLine("┌────────────────────────────────────────────┐");
            Console.WriteLine("│              MAIN MENU                     │");
            Console.WriteLine("├────────────────────────────────────────────┤");
            Console.WriteLine("│  1. Add New Product                        │");
            Console.WriteLine("│  2. Sell Product (Reduce Stock)            │");
            Console.WriteLine("│  3. Restock Product (Add Stock)            │");
            Console.WriteLine("│  4. View All Products                      │");
            Console.WriteLine("│  5. Remove Product                         │");
            Console.WriteLine("│  6. Exit                                   │");
            Console.WriteLine("└────────────────────────────────────────────┘");
            Console.Write("\nSelect an option (1-6): ");
        }

        static void AddNewProduct(InventoryService service)
        {
            Console.WriteLine("═══ ADD NEW PRODUCT ═══\n");

            string name = GetStringInput("Product Name: ");
            decimal price = GetDecimalInput("Price (£): ");
            int quantity = GetIntInput("Stock Quantity: ");

            service.DisplayProductDetails(name, price, quantity);
            
            if (GetConfirmation("Add new product?"))
            {
                service.AddProduct(name, price, quantity);
            }
            else
            {
                Console.WriteLine("❌ Product addition cancelled.");
            }
        }

        static void SellProduct(InventoryService service)
        {
            Console.WriteLine("═══ SELL PRODUCT ═══\n");

            int id = GetIntInput("Product ID: ");
            int quantity = GetIntInput("Quantity to Sell: ");

            service.SellProduct(id, quantity);
        }

        static void RestockProduct(InventoryService service)
        {
            Console.WriteLine("═══ RESTOCK PRODUCT ═══\n");

            int id = GetIntInput("Product ID: ");
            int quantity = GetIntInput("Quantity to Add: ");

            service.RestockProduct(id, quantity);
        }

        static void RemoveProduct(InventoryService service)
        {
            Console.WriteLine("═══ REMOVE PRODUCT ═══\n");
            service.ViewAllProducts();
            int id = GetIntInput("Product ID to Remove: ");

            // Show product details before confirming deletion
            var product = service.GetProductById(id);
            if (product == null)
            {
                Console.WriteLine($"❌ Error: Product with ID {id} not found.");
                return;
            }

            Console.WriteLine($"\nProduct to delete: {product}\n");

            if (GetConfirmation("Are you sure you want to delete this product?"))
            {
                service.RemoveProduct(id);
            }
            else
            {
                Console.WriteLine("❌ Product deletion cancelled.");
            }
        }

        // ═══════════════════════════════════════════════════════════
        // HELPER METHODS
        // ═══════════════════════════════════════════════════════════

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
                
                Console.WriteLine("❌ Error: Input cannot be empty.\n");
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
                
                Console.WriteLine("❌ Error: Please enter a valid non-negative number.\n");
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
                
                Console.WriteLine("❌ Error: Please enter a valid non-negative number.\n");
            }
        }

        static bool GetConfirmation(string message)
        {
            while (true)
            {
                Console.Write($"{message} (Y/N): ");
                string? input = Console.ReadLine()?.Trim().ToUpper();
                Console.WriteLine();

                if (input == "Y") return true;
                if (input == "N") return false;
                
                Console.WriteLine("❌ Invalid input. Please enter Y or N.\n");
            }
        }
    }
}