using IxClouds.API.DTOs.Request;
using IxClouds.API.DTOs.Response;

namespace IxClouds.Domain.Interfaces.Services
{
    public interface ISaleService
    {
        Task<SaleResponseDto> CreateSaleAsync(CreateSaleRequestDto dto);
        Task<List<SaleResponseDto>> GetSalesAsync(DateTime? fromDate, DateTime? toDate);
        Task<SaleResponseDto?> GetSaleByIdAsync(int id);
    }
}