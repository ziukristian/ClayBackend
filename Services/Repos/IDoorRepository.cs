using ClayBackend.Entities;
using ClayBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClayBackend.Services.Repos
{
    public interface IDoorRepository
    {
        Task<IEnumerable<Door>> GetDoorsAsync();
        Task<(IEnumerable<Door>, PaginationData)> GetDoorsAsync(int pageNumber, int pageSize);
        Task<Door> GetDoorAsync(Guid id);
        Task<Door> AddDoorAsync(Door newDoor);
        Task<bool> DoorExistsAsync(Guid id);
        Task<Door> UpdateDoorAsync(Door door);
        void RemoveDoorAsync(Guid id);
        Task<IActionResult> OpenDoorAsync(Guid id, Guid userid);
        Task<IActionResult> CloseDoorAsync(Guid id, Guid userid);
        Task<Group> AddGroupPermissionToDoor(Guid id, Guid groupId);
        Task<User> AddUserPermissionToDoor(Guid id, Guid userId);
    }
}
