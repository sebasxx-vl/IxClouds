using IxClouds.Domain.DTOs.Request;
using IxClouds.Domain.DTOs.Response;

namespace IxClouds.Domain.Interfaces.Services
{
    public interface IProductService
    {
        Task<ProductResponseDto> CreateAsync(CreateProductRequestDto dto);
        Task<ProductResponseDto> UpdateAsync(int id, UpdateProductRequestDto dto);
        Task<bool> DeleteAsync(int id);
        Task<ProductResponseDto?> GetByIdAsync(int id);
        Task<PaginatedResponse<ProductResponseDto>> SearchAsync(SearchProductRequestDto filter);
    }

    public class PaginatedResponse<T>
    {
        public List<T> Items { get; set; } = new();
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
        public bool HasNextPage => PageNumber < TotalPages;
        public bool HasPreviousPage => PageNumber > 1;
    }
}