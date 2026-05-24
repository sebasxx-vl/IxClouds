using IxClouds.Domain.Entities;
using IxClouds.Domain.Enums;
using IxClouds.Domain.Interfaces.Repositories;
using IxClouds.Domain.Interfaces.Service;
namespace IxClouds.Domain.Service;
public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    public ProductService(IProductRepository productRepository) { _productRepository = productRepository; }
    public async Task<IEnumerable<Product>> GetAllProductsAsync() => await _productRepository.GetAllAsync();
    public async Task<Product?> GetProductByIdAsync(int id) => await _productRepository.GetByIdAsync(id);
    public async Task<Product> CreateProductAsync(Product product) { product.CreatedAt = DateTime.Now; return await _productRepository.CreateAsync(product); }
    public async Task UpdateProductAsync(Product product) => await _productRepository.UpdateAsync(product);
    public async Task DeleteProductAsync(int id) => await _productRepository.DeleteAsync(id);
    public async Task<IEnumerable<Product>> SearchProductsAsync(string? brand, string? phoneModel, string? material, string? gender) => await _productRepository.SearchAsync(brand, phoneModel, material, gender);
    public async Task<bool> UpdateStockAsync(int productId, int quantityToSubtract)
    {
        var product = await _productRepository.GetByIdAsync(productId);
        if (product == null || product.Stock < quantityToSubtract) return false;
        product.Stock -= quantityToSubtract;
        await _productRepository.UpdateAsync(product);
        return true;
    }
    public async Task<StockStatus> GetStockStatusAsync(int productId)
    {
        var product = await _productRepository.GetByIdAsync(productId);
        if (product == null) return StockStatus.Critical;
        return product.Stock switch { <= 2 => StockStatus.Critical, <= 10 => StockStatus.Low, _ => StockStatus.Normal };
    }
    public async Task<IEnumerable<Product>> GetLowStockProductsAsync() { var all = await _productRepository.GetAllAsync(); return all.Where(p => p.Stock <= 10); }
    public async Task<int> GetTotalInventoryCountAsync() => await _productRepository.GetTotalStockAsync();
}
