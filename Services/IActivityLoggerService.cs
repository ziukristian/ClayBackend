using ClayBackend.Entities;

namespace ClayBackend.Services
{
    public interface IActivityLoggerService
    {
        Task LogActivityAsync(Guid doorId, Guid userId, string action);
        Task<(IList<ActivityLog>, PaginationData)> GetActivityLogsAsync(int pageNumber, int pageSize);
        Task<bool> CanAccessLogs(Guid userId);
    }
}
