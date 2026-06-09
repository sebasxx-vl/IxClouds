namespace IxClouds.Domain.DTOs.Response
{
    public class DashboardStatsResponseDto
    {
        public decimal TotalSales { get; set; }
        public decimal TodaySales { get; set; }
        public int TodaySalesCount { get; set; }
        public int TotalProducts { get; set; }
        public int LowStockProducts { get; set; }
        public int TotalTransactions { get; set; }
        public decimal MonthlyRevenue { get; set; }
        public List<TopProductResponseDto> TopProducts { get; set; } = new();
        public List<RecentSaleResponseDto> RecentSales { get; set; } = new();
        public decimal EstimatedProfit { get; set; }
        public int TotalProductsSold { get; set; }
    }

    public class TopProductResponseDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int TotalQuantitySold { get; set; }
        public decimal TotalRevenue { get; set; }
    }

    public class RecentSaleResponseDto
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public decimal FinalAmount { get; set; }
        public DateTime SaleDate { get; set; }
        public int ItemsCount { get; set; }
    }
}