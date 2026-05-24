using IxCloud.DataAccess.Context;
using IxClouds.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace IxCloud.DataAccess.Seeders;
public static class DatabaseSeeder
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        if (await context.Products.AnyAsync()) return;
        var products = new List<Product>
        {
            new() { Brand = "Samsung", PhoneModel = "Galaxy S21", Gender = "Unisex", Material = "Silicona", Stock = 15, PurchasePrice = 10000, SalePrice = 20000, ImageUrl = "", CreatedAt = DateTime.Now },
            new() { Brand = "Apple", PhoneModel = "iPhone 13", Gender = "Unisex", Material = "Cuero", Stock = 8, PurchasePrice = 15000, SalePrice = 30000, ImageUrl = "", CreatedAt = DateTime.Now },
            new() { Brand = "Xiaomi", PhoneModel = "Redmi Note 10", Gender = "Unisex", Material = "Plastico", Stock = 2, PurchasePrice = 8000, SalePrice = 15000, ImageUrl = "", CreatedAt = DateTime.Now }
        };
        context.Products.AddRange(products);
        await context.SaveChangesAsync();
    }
}
