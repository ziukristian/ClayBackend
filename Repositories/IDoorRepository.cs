using ClayBackend.Entities;
using ClayBackend.Models;
using ClayBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClayBackend.Repos
{
    public interface IDoorRepository
    {
        Task<IList<Door>> GetDoorsAsync();
        Task<(IList<Door>, PaginationData)> GetDoorsAsync(int pageNumber, int pageSize);
        Task<Door?> GetDoorByIdAsync(Guid id);
        Task<Door> AddDoorAsync(Door newDoor);
        Task<bool> DoorExistsAsync(Guid id);
        Task<Door> UpdateDoorAsync(Door door);
        Task RemoveDoorByIdAsync(Guid id);
        Task<bool> OpenDoorAsync(Guid id, Guid userid);
        Task<bool> CloseDoorAsync(Guid id, Guid userid);
        Task<Group> AddGroupPermissionToDoorAsync(Guid id, Guid groupId);
        Task<User> AddUserPermissionToDoor(Guid id, Guid userId);
        Task RemoveGroupPermissionFromDoorAsync(Guid id, Guid groupId);
        Task RemoveUserPermissionFromDoorAsync(Guid id, Guid userId);
    }
}
