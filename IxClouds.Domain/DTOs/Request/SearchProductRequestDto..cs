namespace IxClouds.Domain.DTOs.Request
{
    public class SearchProductRequestDto
    {
        public string? SearchTerm { get; set; }
        public string? Category { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public bool? LowStockOnly { get; set; }
        public bool? IsActive { get; set; } = true;
        public string? SortBy { get; set; } = "name";
        public bool SortDescending { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}