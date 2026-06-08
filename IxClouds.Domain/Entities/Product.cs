using System.Net.ServerSentEvents;

namespace IxClouds.Domain.Entities;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Sku { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal Cost { get; set; }
    public int StockQuantity { get; set; }
    public int MinStockLevel { get; set; } = 10;
    public string Category { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    public ICollection<SaleItem> SaleItems { get; set; } = new List<SaleItem>();

    public bool HasLowStock() => StockQuantity <= MinStockLevel;
    public decimal GetProfitMargin() => Price > 0 ? ((Price - Cost) / Price) * 100 : 0;
}
