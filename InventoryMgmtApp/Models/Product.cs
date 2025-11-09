namespace InventoryMgmtApp.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Display product in a readable format
        public override string ToString()
        {
            return $"ID: {Id,-5} | Name: {Name,-20} | Price: £{Price,-10:F2} | Quantity: {StockQuantity,-5}";
        }
    }
}