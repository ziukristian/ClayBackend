
using ClayBackend.Context;
using ClayBackend.Entities;
using ClayBackend.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ClayBackend.Repositories
{
    public class UserRepository(AppDbContext context) : IUserRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            return await _context.Users
                .Include(u => u.Groups)
                .Include(u => u.Permissions)
                .Include(u => u.ActivityLogs)
                .AsSplitQuery()
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<(IList<User>, PaginationData)> GetUsersAsync(int pageNumber, int pageSize)
        {
            var collection = _context.Users as IQueryable<User>;

            var itemCount = await collection.CountAsync();

            var paginationData = new PaginationData(itemCount, pageSize, pageNumber);

            var collectionToReturn = await collection
                .OrderBy(c => c.UserName)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (collectionToReturn, paginationData);
        }

        public async Task<bool> UserExistsAsync(Guid id)
        {
            return await _context.Users.AnyAsync(d => d.Id == id);
        }
    }
}
