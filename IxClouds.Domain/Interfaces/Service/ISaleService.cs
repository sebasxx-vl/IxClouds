using IxClouds.Domain.DTOs.Request;
using IxClouds.Domain.DTOs.Response;

namespace IxClouds.Domain.Interfaces.Services
{
    public interface ISaleService
    {
        Task<SaleResponseDto> CreateSaleAsync(CreateSaleRequestDto dto);
        Task<List<SaleResponseDto>> GetSalesAsync(DateTime? fromDate, DateTime? toDate);
        Task<SaleResponseDto?> GetSaleByIdAsync(int id);
    }
}