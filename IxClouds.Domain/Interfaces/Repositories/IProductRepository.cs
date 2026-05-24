using IxClouds.Domain.Entities;
namespace IxClouds.Domain.Interfaces.Repositories;
public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(int id);
    Task<Product> CreateAsync(Product product);
    Task UpdateAsync(Product product);
    Task DeleteAsync(int id);
    Task<IEnumerable<Product>> SearchAsync(string? brand, string? phoneModel, string? material, string? gender);
    Task<bool> ExistsAsync(int id);
    Task<int> GetTotalStockAsync();
}
