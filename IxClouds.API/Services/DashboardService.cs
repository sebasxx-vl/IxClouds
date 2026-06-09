using IxCloud.DataAccess;
using IxCloud.DataAccess.Context;
using IxClouds.Domain.DTOs.Response;
using IxClouds.Domain.Interfaces.Services;
using Microsoft.EntityFrameworkCore;

namespace IxClouds.API.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly ApplicationDbContext _context;

        public DashboardService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardStatsResponseDto> GetDashboardStatsAsync()
        {
            var today = DateTime.UtcNow.Date;
            var firstDayOfMonth = new DateTime(today.Year, today.Month, 1);

            var totalSales = await _context.Sales.SumAsync(s => s.FinalAmount);
            var todaySales = await _context.Sales.Where(s => s.SaleDate.Date == today).SumAsync(s => s.FinalAmount);
            var todaySalesCount = await _context.Sales.Where(s => s.SaleDate.Date == today).CountAsync();
            var totalProducts = await _context.Products.CountAsync(p => p.IsActive);
            var lowStockProducts = await _context.Products.Where(p => p.StockQuantity <= p.MinStockLevel && p.IsActive).CountAsync();
            var totalTransactions = await _context.Sales.CountAsync();
            var monthlyRevenue = await _context.Sales.Where(s => s.SaleDate >= firstDayOfMonth).SumAsync(s => s.FinalAmount);

            // Usar SaleItems DbSet directamente
            var topProducts = await _context.SaleItems
                .Include(si => si.Product)
                .Where(si => si.Product != null)
                .GroupBy(si => new { si.ProductId, si.Product!.Name })
                .Select(g => new TopProductResponseDto
                {
                    ProductId = g.Key.ProductId,
                    ProductName = g.Key.Name,
                    TotalQuantitySold = g.Sum(x => x.Quantity),
                    TotalRevenue = g.Sum(x => x.TotalPrice)
                })
                .OrderByDescending(x => x.TotalQuantitySold)
                .Take(5)
                .ToListAsync();

            var recentSales = await _context.Sales
                .Include(s => s.Items)
                .OrderByDescending(s => s.SaleDate)
                .Take(5)
                .Select(s => new RecentSaleResponseDto
                {
                    Id = s.Id,
                    InvoiceNumber = s.InvoiceNumber,
                    CustomerName = s.CustomerName,
                    FinalAmount = s.FinalAmount,
                    SaleDate = s.SaleDate,
                    ItemsCount = s.Items.Count
                })
                .ToListAsync();

            var estimatedProfit = await _context.SaleItems
                .Include(si => si.Product)
                .Where(si => si.Product != null)
                .SumAsync(si => (si.UnitPrice - si.Product!.Cost) * si.Quantity);

            var totalProductsSold = await _context.SaleItems.SumAsync(si => si.Quantity);

            return new DashboardStatsResponseDto
            {
                TotalSales = totalSales,
                TodaySales = todaySales,
                TodaySalesCount = todaySalesCount,
                TotalProducts = totalProducts,
                LowStockProducts = lowStockProducts,
                TotalTransactions = totalTransactions,
                MonthlyRevenue = monthlyRevenue,
                TopProducts = topProducts,
                RecentSales = recentSales,
                EstimatedProfit = estimatedProfit,
                TotalProductsSold = totalProductsSold
            };
        }
    }
}