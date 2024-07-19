using ClayBackend.Entities;
using ClayBackend.Services;

namespace ClayBackend.Repositories
{
    public interface IUserRepository
    {
        Task<(IList<User>, PaginationData)> GetUsersAsync(int pageNumber, int pageSize);
        Task<User?> GetUserByIdAsync(Guid id);
        Task<bool> UserExistsAsync(Guid id);
    }
}
