using System.Data.Common;
using InventoryMgmtApp.Models;

namespace InventoryMgmtApp.Services
{
    public class InventoryService
    {
        private List<Product> _products;
        private readonly DataService _dataService;
        private int _nextId;

        public InventoryService(DataService dataService)
        {
            _dataService = dataService;
            _products = _dataService.LoadProducts();
            _nextId = _products.Any() ? _products.Max(p => p.Id) + 1 : 1;
        }

        // Add new product
        public void AddProduct(string name, decimal price, int stockQuantity)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("❌ Error: Product name cannot be empty.");
                    return;
                }

                if (price < 0)
                {
                    Console.WriteLine("❌ Error: Price cannot be negative.");
                    return;
                }

                if (stockQuantity < 0)
                {
                    Console.WriteLine("❌ Error: Stock quantity cannot be negative.");
                    return;
                }

                var product = new Product
                {
                    Id = _nextId++,
                    Name = name,
                    Price = price,
                    StockQuantity = stockQuantity
                };

                _products.Add(product);
                
                if (_dataService.SaveProducts(_products))
                {
                    Console.WriteLine($"✅ Success: Product '{name}' added successfully!");
                    Console.WriteLine($"   ID: {product.Id} | Price: £{price:F2} | Quantity: {stockQuantity}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error adding product: {ex.Message}");
            }
        }

        // Update stock when sold
        public void SellProduct(int id, int quantity)
        {
            try
            {
                var product = _products.FirstOrDefault(p => p.Id == id);
                
                if (product == null)
                {
                    Console.WriteLine($"❌ Error: Product with ID {id} not found.");
                    return;
                }

                if (quantity <= 0)
                {
                    Console.WriteLine("❌ Error: Quantity must be greater than 0.");
                    return;
                }

                if (product.StockQuantity < quantity)
                {
                    Console.WriteLine($"❌ Error: Insufficient stock. Available: {product.StockQuantity}, Requested: {quantity}");
                    return;
                }

                product.StockQuantity -= quantity;
                product.UpdatedAt = DateTime.Now;

                if (_dataService.SaveProducts(_products))
                {
                    Console.WriteLine($"✅ Success: Sold {quantity} unit(s) of '{product.Name}'");
                    Console.WriteLine($"   Remaining stock: {product.StockQuantity}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error selling product: {ex.Message}");
            }
        }

        // Update stock when restocked
        public void RestockProduct(int id, int quantity)
        {
            try
            {
                var product = _products.FirstOrDefault(p => p.Id == id);
                
                if (product == null)
                {
                    Console.WriteLine($"❌ Error: Product with ID {id} not found.");
                    return;
                }

                if (quantity <= 0)
                {
                    Console.WriteLine("❌ Error: Quantity must be greater than 0.");
                    return;
                }

                product.StockQuantity += quantity;
                product.UpdatedAt = DateTime.Now;

                if (_dataService.SaveProducts(_products))
                {
                    Console.WriteLine($"✅ Success: Restocked {quantity} unit(s) of '{product.Name}'");
                    Console.WriteLine($"   New stock level: {product.StockQuantity}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error restocking product: {ex.Message}");
            }
        }

        // View all products
        public void ViewAllProducts()
        {
            if (_products.Count == 0)
            {
                Console.WriteLine("📦 No products in inventory.");
                return;
            }

            Console.WriteLine("\n" + new string('═', 80));
            Console.WriteLine("                          INVENTORY PRODUCTS");
            Console.WriteLine(new string('═', 80));
            
            foreach (var product in _products.OrderBy(p => p.Id))
            {
                Console.WriteLine(product);
            }
            
            Console.WriteLine(new string('═', 80));
            Console.WriteLine($"Total Products: {_products.Count} | Total Stock Value: £{CalculateTotalValue():F2}");
            Console.WriteLine(new string('═', 80));
        }

        // Remove product
        public void RemoveProduct(int id)
        {
            try
            {
                var product = _products.FirstOrDefault(p => p.Id == id);

                if (product == null)
                {
                    Console.WriteLine($"❌ Error: Product with ID {id} not found.");
                    return;
                }

                string productName = product.Name;
                _products.Remove(product);

                if (_dataService.SaveProducts(_products))
                {
                    Console.WriteLine($"✅ Success: Product '{productName}' (ID: {id}) removed from inventory.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error removing product: {ex.Message}");
            }
        }

        public Product? GetProductById(int id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }

        public string DisplayProductDetails(string name, decimal price, int quantity)
        {
            return $"Name: {name,-20} | Price: £{price,-10:F2} | Quantity: {quantity,-5}\n";
        }

        // Helper method to calculate total inventory value
        private decimal CalculateTotalValue()
        {
            return _products.Sum(p => p.Price * p.StockQuantity);
        }

        public int GetProductCount() => _products.Count;
    }
}