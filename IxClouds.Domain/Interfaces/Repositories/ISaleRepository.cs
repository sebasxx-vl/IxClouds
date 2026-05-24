using IxClouds.Domain.Entities;
namespace IxClouds.Domain.Interfaces.Repositories;
public interface ISaleRepository
{
    Task<Sale> CreateSaleAsync(Sale sale);
    Task<IEnumerable<Sale>> GetAllSalesAsync();
    Task<Sale?> GetSaleByIdAsync(int id);
    Task<decimal> GetTotalSalesAsync();
    Task<decimal> GetTodaySalesTotalAsync();
    Task<IEnumerable<(Product Product, int TotalSold)>> GetBestSellingProductsAsync(int topCount);
}
