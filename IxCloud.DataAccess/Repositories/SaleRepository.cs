using IxCloud.DataAccess.Context;
using IxClouds.Domain.Entities;
using IxClouds.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
namespace IxCloud.DataAccess.Repositories;
public class SaleRepository : ISaleRepository
{
    private readonly ApplicationDbContext _context;
    public SaleRepository(ApplicationDbContext context) { _context = context; }
    public async Task<Sale> CreateSaleAsync(Sale sale) { sale.Date = DateTime.Now; _context.Sales.Add(sale); await _context.SaveChangesAsync(); return sale; }
    public async Task<IEnumerable<Sale>> GetAllSalesAsync() => await _context.Sales.Include(s => s.SaleDetails).ThenInclude(d => d.Product).ToListAsync();
    public async Task<Sale?> GetSaleByIdAsync(int id) => await _context.Sales.Include(s => s.SaleDetails).ThenInclude(d => d.Product).FirstOrDefaultAsync(s => s.Id == id);
    public async Task<decimal> GetTotalSalesAsync() => await _context.Sales.SumAsync(s => s.Total);
    public async Task<decimal> GetTodaySalesTotalAsync() { var today = DateTime.Today; return await _context.Sales.Where(s => s.Date >= today && s.Date < today.AddDays(1)).SumAsync(s => s.Total); }
    public async Task<IEnumerable<(Product Product, int TotalSold)>> GetBestSellingProductsAsync(int topCount)
    {
        return await _context.SaleDetails.GroupBy(d => d.Product).Select(g => new { Product = g.Key, TotalSold = g.Sum(d => d.Quantity) }).OrderByDescending(x => x.TotalSold).Take(topCount).Select(x => ValueTuple.Create(x.Product, x.TotalSold)).ToListAsync();
    }
}
