using IxClouds.Domain.DTOs.Response;

namespace IxClouds.Domain.Interfaces.Services
{
    public interface IDashboardService
    {
        Task<DashboardStatsResponseDto> GetDashboardStatsAsync();
    }
}