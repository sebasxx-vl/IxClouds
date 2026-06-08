using IxClouds.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IxClouds.DataAccess
{
    public static class DataSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            if (await context.Products.AnyAsync()) return;

            var products = new List<Product>
            {
                new()
                {
                    Name = "Laptop Dell XPS 15",
                    Sku = "LAP-001",
                    Description = "Laptop premium para profesionales",
                    Price = 1299.99m,
                    Cost = 899.99m,
                    StockQuantity = 45,
                    MinStockLevel = 5,
                    Category = "Electronics",
                    ImageUrl = "https://via.placeholder.com/300x200/6366f1/ffffff?text=Laptop",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow.AddDays(-30),
                    UpdatedAt = DateTime.UtcNow.AddDays(-30)
                },
                new()
                {
                    Name = "Monitor 4K LG 27 pulgadas",
                    Sku = "MON-002",
                    Description = "Monitor Ultra HD profesional",
                    Price = 449.99m,
                    Cost = 299.99m,
                    StockQuantity = 8,
                    MinStockLevel = 10,
                    Category = "Electronics",
                    ImageUrl = "https://via.placeholder.com/300x200/14b8a6/ffffff?text=Monitor",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow.AddDays(-25),
                    UpdatedAt = DateTime.UtcNow.AddDays(-25)
                },
                new()
                {
                    Name = "Teclado Mecánico RGB",
                    Sku = "KEY-003",
                    Description = "Teclado mecánico gaming",
                    Price = 129.99m,
                    Cost = 59.99m,
                    StockQuantity = 120,
                    MinStockLevel = 15,
                    Category = "Accessories",
                    ImageUrl = "https://via.placeholder.com/300x200/f59e0b/ffffff?text=Keyboard",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow.AddDays(-20),
                    UpdatedAt = DateTime.UtcNow.AddDays(-20)
                },
                new()
                {
                    Name = "Mouse Inalámbrico Logitech",
                    Sku = "MOU-004",
                    Description = "Mouse ergonómico inalámbrico",
                    Price = 79.99m,
                    Cost = 35.99m,
                    StockQuantity = 3,
                    MinStockLevel = 15,
                    Category = "Accessories",
                    ImageUrl = "https://via.placeholder.com/300x200/ef4444/ffffff?text=Mouse",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow.AddDays(-15),
                    UpdatedAt = DateTime.UtcNow.AddDays(-15)
                },
                new()
                {
                    Name = "Hub USB-C 7 en 1",
                    Sku = "USB-005",
                    Description = "Adaptador multi-puerto USB-C",
                    Price = 59.99m,
                    Cost = 24.99m,
                    StockQuantity = 67,
                    MinStockLevel = 10,
                    Category = "Accessories",
                    ImageUrl = "https://via.placeholder.com/300x200/8b5cf6/ffffff?text=USB+Hub",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow.AddDays(-10),
                    UpdatedAt = DateTime.UtcNow.AddDays(-10)
                },
                new()
                {
                    Name = "Webcam HD 1080p",
                    Sku = "WEB-006",
                    Description = "Cámara web full HD",
                    Price = 49.99m,
                    Cost = 22.99m,
                    StockQuantity = 0,
                    MinStockLevel = 8,
                    Category = "Accessories",
                    ImageUrl = "https://via.placeholder.com/300x200/ec4899/ffffff?text=Webcam",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow.AddDays(-5),
                    UpdatedAt = DateTime.UtcNow.AddDays(-5)
                }
            };

            await context.Products.AddRangeAsync(products);
            await context.SaveChangesAsync();

            // Seed demo sales
            var random = new Random();
            var sales = new List<Sale>();

            for (int i = 0; i < 50; i++)
            {
                var product = products[random.Next(products.Count)];
                var qty = random.Next(1, Math.Min(5, product.StockQuantity + 1));

                if (qty == 0) continue;

                var sale = new Sale
                {
                    InvoiceNumber = $"INV-2024-{(i + 1):D4}",
                    SaleDate = DateTime.UtcNow.AddDays(-random.Next(0, 30)).AddHours(-random.Next(0, 24)),
                    TotalAmount = product.Price * qty,
                    Discount = 0,
                    FinalAmount = product.Price * qty,
                    PaymentMethod = random.Next(3) switch { 0 => "Cash", 1 => "Card", _ => "Transfer" },
                    CustomerName = new[] { "Juan Pérez", "María García", "Carlos López", "Ana Martínez", "General" }[random.Next(5)],
                    Notes = "Venta demo generada automáticamente",
                    Items = new List<SaleItem>
                    {
                        new()
                        {
                            ProductId = product.Id,
                            Quantity = qty,
                            UnitPrice = product.Price,
                            TotalPrice = product.Price * qty
                        }
                    }
                };

                sales.Add(sale);
            }

            await context.Sales.AddRangeAsync(sales);
            await context.SaveChangesAsync();
        }
    }
}