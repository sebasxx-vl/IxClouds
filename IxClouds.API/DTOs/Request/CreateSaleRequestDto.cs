namespace IxClouds.API.DTOs.Request;
public class CreateSaleRequestDto { public List<SaleItemDto> Items { get; set; } = new(); }
public class SaleItemDto { public int ProductId { get; set; } public int Quantity { get; set; } }
