namespace IxClouds.API.DTOs.Response;
public class DashboardStatsResponseDto
{
    public decimal TotalSalesAmount { get; set; }
    public decimal TodaySalesAmount { get; set; }
    public int TotalInventoryCount { get; set; }
    public int TotalSalesCount { get; set; }
    public List<BestSellerDto> BestSellingProducts { get; set; } = new();
    public List<ProductResponseDto> LowStockProducts { get; set; } = new();
}
public class BestSellerDto { public int ProductId { get; set; } public string ProductName { get; set; } = string.Empty; public int TotalSold { get; set; } public decimal TotalRevenue { get; set; } }
