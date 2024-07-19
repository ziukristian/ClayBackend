using ClayBackend.Entities;

namespace ClayBackend.Services
{
    public interface IActivityLoggerService
    {
        Task LogActivityAsync(Guid doorId, Guid userId, string action);
    }
}
