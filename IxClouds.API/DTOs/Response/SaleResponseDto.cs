namespace IxClouds.API.DTOs.Response;
public class SaleResponseDto { public int Id { get; set; } public DateTime Date { get; set; } public decimal Total { get; set; } public List<SaleDetailResponseDto> Items { get; set; } = new(); }
public class SaleDetailResponseDto { public int ProductId { get; set; } public string ProductName { get; set; } = string.Empty; public int Quantity { get; set; } public decimal UnitPrice { get; set; } public decimal Subtotal { get; set; } }
