using IxCloud.DataAccess.Context;
using IxClouds.Domain.Entities;
using IxClouds.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
namespace IxCloud.DataAccess.Repositories;
public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;
    public ProductRepository(ApplicationDbContext context) { _context = context; }
    public async Task<IEnumerable<Product>> GetAllAsync() => await _context.Products.ToListAsync();
    public async Task<Product?> GetByIdAsync(int id) => await _context.Products.FindAsync(id);
    public async Task<Product> CreateAsync(Product product) { _context.Products.Add(product); await _context.SaveChangesAsync(); return product; }
    public async Task UpdateAsync(Product product) { _context.Entry(product).State = EntityState.Modified; await _context.SaveChangesAsync(); }
    public async Task DeleteAsync(int id) { var product = await _context.Products.FindAsync(id); if (product != null) { _context.Products.Remove(product); await _context.SaveChangesAsync(); } }
    public async Task<IEnumerable<Product>> SearchAsync(string? brand, string? phoneModel, string? material, string? gender)
    {
        var query = _context.Products.AsQueryable();
        if (!string.IsNullOrWhiteSpace(brand)) query = query.Where(p => p.Brand.Contains(brand));
        if (!string.IsNullOrWhiteSpace(phoneModel)) query = query.Where(p => p.PhoneModel.Contains(phoneModel));
        if (!string.IsNullOrWhiteSpace(material)) query = query.Where(p => p.Material.Contains(material));
        if (!string.IsNullOrWhiteSpace(gender)) query = query.Where(p => p.Gender.Contains(gender));
        return await query.ToListAsync();
    }
    public async Task<bool> ExistsAsync(int id) => await _context.Products.AnyAsync(p => p.Id == id);
    public async Task<int> GetTotalStockAsync() => await _context.Products.SumAsync(p => p.Stock);
}
