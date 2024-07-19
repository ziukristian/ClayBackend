using ClayBackend.Entities;
using ClayBackend.Services;

namespace ClayBackend.Repositories
{
    public interface IActivityLoggerRepository
    {
        Task<(IList<ActivityLog>, PaginationData)> GetActivityLogsAsync(int pageNumber, int pageSize);
        Task<bool> CanAccessLogs(Guid userId);
    }
}
