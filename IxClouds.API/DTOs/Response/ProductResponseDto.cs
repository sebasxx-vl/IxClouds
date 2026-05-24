namespace IxClouds.API.DTOs.Response;
public class ProductResponseDto
{
    public int Id { get; set; }
    public string Brand { get; set; } = string.Empty;
    public string PhoneModel { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty;
    public string Material { get; set; } = string.Empty;
    public int Stock { get; set; }
    public decimal PurchasePrice { get; set; }
    public decimal SalePrice { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public string StockStatus { get; set; } = string.Empty;
}
