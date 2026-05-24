using IxClouds.Domain.Entities;
using IxClouds.Domain.Interfaces.Repositories;
using IxClouds.Domain.Interfaces.Service;
namespace IxClouds.Domain.Service;
public class SaleService : ISaleService
{
    private readonly ISaleRepository _saleRepository;
    private readonly IProductService _productService;
    public SaleService(ISaleRepository saleRepository, IProductService productService) { _saleRepository = saleRepository; _productService = productService; }
    public async Task<Sale> RegisterSaleAsync(Sale sale, List<(int ProductId, int Quantity)> items)
    {
        if (items == null || !items.Any()) throw new ArgumentException("La venta debe tener al menos un producto");
        decimal total = 0;
        var saleDetails = new List<SaleDetail>();
        foreach (var item in items)
        {
            var product = await _productService.GetProductByIdAsync(item.ProductId);
            if (product == null) throw new ArgumentException($"Producto con ID {item.ProductId} no encontrado");
            if (product.Stock < item.Quantity) throw new InvalidOperationException($"Stock insuficiente para {product.Brand} {product.PhoneModel}");
            var subtotal = product.SalePrice * item.Quantity;
            total += subtotal;
            saleDetails.Add(new SaleDetail { ProductId = item.ProductId, Quantity = item.Quantity, UnitPrice = product.SalePrice, Subtotal = subtotal });
            await _productService.UpdateStockAsync(item.ProductId, item.Quantity);
        }
        sale.Total = total;
        sale.SaleDetails = saleDetails;
        return await _saleRepository.CreateSaleAsync(sale);
    }
    public async Task<IEnumerable<Sale>> GetAllSalesAsync() => await _saleRepository.GetAllSalesAsync();
    public async Task<Sale?> GetSaleByIdAsync(int id) => await _saleRepository.GetSaleByIdAsync(id);
    public async Task<decimal> GetTotalSalesAmountAsync() => await _saleRepository.GetTotalSalesAsync();
    public async Task<decimal> GetTodaySalesAmountAsync() => await _saleRepository.GetTodaySalesTotalAsync();
    public async Task<IEnumerable<(Product Product, int TotalSold)>> GetBestSellingProductsAsync(int topCount) => await _saleRepository.GetBestSellingProductsAsync(topCount);
}
