
namespace IxClouds.API.Services
{
    public interface IDashboardService
    {
        Task<DashboardStatsResponseDto> GetDashboardStatsAsync();
    }
}