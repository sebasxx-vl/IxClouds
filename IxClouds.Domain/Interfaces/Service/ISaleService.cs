using IxClouds.Domain.Entities;
namespace IxClouds.Domain.Interfaces.Service;
public interface ISaleService
{
    Task<Sale> RegisterSaleAsync(Sale sale, List<(int ProductId, int Quantity)> items);
    Task<IEnumerable<Sale>> GetAllSalesAsync();
    Task<Sale?> GetSaleByIdAsync(int id);
    Task<decimal> GetTotalSalesAmountAsync();
    Task<decimal> GetTodaySalesAmountAsync();
    Task<IEnumerable<(Product Product, int TotalSold)>> GetBestSellingProductsAsync(int topCount);
}
