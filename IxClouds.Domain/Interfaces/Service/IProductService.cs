using IxClouds.Domain.Entities;
using IxClouds.Domain.Enums;
namespace IxClouds.Domain.Interfaces.Service;
public interface IProductService
{
    Task<IEnumerable<Product>> GetAllProductsAsync();
    Task<Product?> GetProductByIdAsync(int id);
    Task<Product> CreateProductAsync(Product product);
    Task UpdateProductAsync(Product product);
    Task DeleteProductAsync(int id);
    Task<IEnumerable<Product>> SearchProductsAsync(string? brand, string? phoneModel, string? material, string? gender);
    Task<bool> UpdateStockAsync(int productId, int quantityToSubtract);
    Task<StockStatus> GetStockStatusAsync(int productId);
    Task<IEnumerable<Product>> GetLowStockProductsAsync();
    Task<int> GetTotalInventoryCountAsync();
}
