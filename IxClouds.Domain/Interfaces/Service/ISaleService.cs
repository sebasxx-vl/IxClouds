namespace IxClouds.API.Services
{
    public interface ISaleService
    {
        Task<SaleResponseDto> CreateSaleAsync(CreateSaleRequestDto dto);
        Task<List<SaleResponseDto>> GetSalesAsync(DateTime? fromDate, DateTime? toDate);
        Task<SaleResponseDto?> GetSaleByIdAsync(int id);
    }
}
