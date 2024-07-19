using ClayBackend.Context;
using ClayBackend.Entities;
using ClayBackend.Services;
using Microsoft.EntityFrameworkCore;

namespace ClayBackend.Repositories
{
    public class ActivityLoggerRepository(AppDbContext context, IActivityLoggerService activityLoggerService) : IActivityLoggerRepository
    {
        private readonly IActivityLoggerService _activityLoggerService = activityLoggerService;
        private readonly AppDbContext _context = context;
        public async Task<bool> CanAccessLogs(Guid userId)
        {
            return await _context.GroupMemberships
                .Where(m => m.UserId == userId)
                .Select(m => m.Group)
                .AnyAsync(g => g.CanMonitor);
        }

        public async Task<(IList<ActivityLog>, PaginationData)> GetActivityLogsAsync(int pageNumber, int pageSize)
        {
            var collection = _context.ActivityLogs as IQueryable<ActivityLog>;

            var itemCount = await collection.CountAsync();

            var paginationData = new PaginationData(itemCount, pageSize, pageNumber);

            var collectionToReturn = await collection
                .Include(a => a.User)
                .Include(a => a.Door)
                .OrderByDescending(a => a.TimeStamp)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (collectionToReturn, paginationData);
        }
    }
}
