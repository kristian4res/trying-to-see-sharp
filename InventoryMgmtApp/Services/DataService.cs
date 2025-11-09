using System.Text.Json;
using InventoryMgmtApp.Models;

namespace InventoryMgmtApp.Services
{
    public class DataService
    {
        private readonly string _filePath;

        public DataService(string filePath = "Data/inventory.json")
        {
            _filePath = filePath;
        }

        public List<Product> LoadProducts()
        {
            try
            {
                if (!File.Exists(_filePath))
                {
                    return new List<Product>();
                }

                string json = File.ReadAllText(_filePath);
                return JsonSerializer.Deserialize<List<Product>>(json) ?? new List<Product>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️  Error loading data: {ex.Message}");
                return new List<Product>();
            }
        }

        public bool SaveProducts(List<Product> products)
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(products, options);
                File.WriteAllText(_filePath, json);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️  Error saving data: {ex.Message}");
                return false;
            }
        }
    }
}