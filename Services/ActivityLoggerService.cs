using ClayBackend.Context;
using ClayBackend.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClayBackend.Services
{
    public class ActivityLoggerService(AppDbContext context) : IActivityLoggerService
    {
        private readonly AppDbContext _context = context;

        public async Task<bool> CanAccessLogs(Guid userId)
        {
            // Find the user's groups and check if any of them have the permission to access logs
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
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (collectionToReturn, paginationData);
        }

        public async Task LogActivityAsync(Guid doorId, Guid userId, string action)
        {
            // Log activity into database
            await _context.ActivityLogs.AddAsync(new ActivityLog
            {
                DoorId = doorId,
                UserId = userId,
                Action = action
            });

            await _context.SaveChangesAsync();
        }
    }
}
