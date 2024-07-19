using ClayBackend.Context;
using ClayBackend.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClayBackend.Services
{
    public class ActivityLoggerService(AppDbContext context) : IActivityLoggerService
    {
        private readonly AppDbContext _context = context;
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
