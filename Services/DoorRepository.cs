using ClayBackend.Context;
using ClayBackend.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClayBackend.Services
{
    public class DoorRepository(IHttpContextAccessor httpContextAccessor, AppDbContext context) : IDoorRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly AppDbContext _context = context;

        public async Task<IEnumerable<Door>> GetDoorsAsync()
        {
            return await _context.Doors.OrderBy(c => c.Description).ToListAsync();
        }

        public async Task<(IEnumerable<Door>, PaginationData)> GetDoorsAsync(int pageNumber, int pageSize)
        {
            var collection = _context.Doors as IQueryable<Door>;

            var itemCount = await collection.CountAsync();

            var paginationData = new PaginationData(itemCount, pageSize, pageNumber);

            var collectionToReturn = await collection.OrderBy(c => c.Description)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (collectionToReturn, paginationData);
        }
    }
}
