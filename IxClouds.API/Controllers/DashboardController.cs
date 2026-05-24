using IxClouds.API.DTOs.Response;
using IxClouds.Domain.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;
namespace IxClouds.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class DashboardController : ControllerBase
{
    private readonly ISaleService _saleService;
    private readonly IProductService _productService;
    public DashboardController(ISaleService saleService, IProductService productService) { _saleService = saleService; _productService = productService; }
    [HttpGet("stats")] public async Task<ActionResult<DashboardStatsResponseDto>> GetDashboardStats()
    {
        var totalSales = await _saleService.GetTotalSalesAmountAsync();
        var todaySales = await _saleService.GetTodaySalesAmountAsync();
        var totalInventory = await _productService.GetTotalInventoryCountAsync();
        var allSales = await _saleService.GetAllSalesAsync();
        var bestSellers = await _saleService.GetBestSellingProductsAsync(5);
        var lowStock = await _productService.GetLowStockProductsAsync();
        return Ok(new DashboardStatsResponseDto
        {
            TotalSalesAmount = totalSales,
            TodaySalesAmount = todaySales,
            TotalInventoryCount = totalInventory,
            TotalSalesCount = allSales.Count(),
            BestSellingProducts = bestSellers.Select(bs => new BestSellerDto { ProductId = bs.Product.Id, ProductName = $"{bs.Product.Brand} {bs.Product.PhoneModel}", TotalSold = bs.TotalSold, TotalRevenue = bs.TotalSold * bs.Product.SalePrice }).ToList(),
            LowStockProducts = lowStock.Select(p => new ProductResponseDto { Id = p.Id, Brand = p.Brand, PhoneModel = p.PhoneModel, Stock = p.Stock, SalePrice = p.SalePrice, StockStatus = p.Stock <= 2 ? "Critical" : "Low" }).ToList()
        });
    }
}
