using ClayBackend.Entities;

namespace ClayBackend.Services
{
    public interface IDoorRepository
    {
        Task<IEnumerable<Door>> GetDoorsAsync();
        Task<(IEnumerable<Door>, PaginationData)> GetDoorsAsync(int pageNumber, int pageSize);
    }
}
