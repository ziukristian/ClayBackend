using ClayBackend.Context;
using ClayBackend.Entities;
using ClayBackend.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using Group = ClayBackend.Entities.Group;

namespace ClayBackend.Services.Repos
{
    public class DoorRepository(AppDbContext context, IActivityLoggerService activityLoggerService) : IDoorRepository
    {
        private readonly AppDbContext _context = context;
        private readonly IActivityLoggerService _activityLoggerService = activityLoggerService;

        public async Task<IList<Door>> GetDoorsAsync()
        {
            return await _context.Doors.OrderBy(c => c.Description).ToListAsync();
        }

        public async Task<(IList<Door>, PaginationData)> GetDoorsAsync(int pageNumber, int pageSize)
        {
            var collection = _context.Doors as IQueryable<Door>;

            var itemCount = await collection.CountAsync();

            var paginationData = new PaginationData(itemCount, pageSize, pageNumber);

            var collectionToReturn = await collection
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (collectionToReturn, paginationData);
        }

        public async Task<Door> AddDoorAsync(Door newDoor)
        {
            _context.Doors.Add(newDoor);
            await _context.SaveChangesAsync();

            return newDoor;
        }

        public async Task<Door> GetDoorAsync(Guid id)
        {
            return await _context.Doors.FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<bool> DoorExistsAsync(Guid id)
        {
            return await _context.Doors.AnyAsync(d => d.Id == id);
        }

        public async Task<Door> UpdateDoorAsync(Door door)
        {
            _context.Doors.Update(door);
            await _context.SaveChangesAsync();

            return door;
        }

        public async Task RemoveDoorByIdAsync(Guid id)
        {
            var door = _context.Doors.FirstOrDefault(d => d.Id == id);

            if (door != null)
            {
                _context.Doors.Remove(door);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> OpenDoorAsync(Guid id, Guid userid)
        {
            // check if id and user id can be found in the user permissions
            var door = _context.Doors.FirstOrDefault(d => d.Id == id);
            // check if any UserPermissions have UserId equal to userid and Id equal to doorid
            bool permittedEntry = _context.UserPermissions.Any(up => up.UserId == userid && up.DoorId == id);

            if(!permittedEntry)
            {
                permittedEntry = await _context.GroupMemberships
                    .Where(gm => gm.UserId == userid)
                    .Join(_context.GroupPermissions,
                        gm => gm.GroupId,
                        gp => gp.GroupId,
                        (gm, gp) => new { gp.DoorId })
                    .AnyAsync(g => g.DoorId == id);
            }

            if (!permittedEntry)
            {
                _activityLoggerService.LogActivityAsync(id, userid, DoorActivityTypes.OpenDenied);
                return false;
            }

            door.IsOpen = true;
            _context.SaveChanges();

            _activityLoggerService.LogActivityAsync(id, userid, DoorActivityTypes.OpenSuccess);

            return true;
        }

        public async Task<bool> CloseDoorAsync(Guid id, Guid userid)
        {
            var door = _context.Doors.FirstOrDefault(d => d.Id == id);

            if (door == null)
            {
                return false;
            }

            door.IsOpen = false;
            await _context.SaveChangesAsync();

            await _activityLoggerService.LogActivityAsync(id, userid, DoorActivityTypes.Closed);

            return true;
        }

        public async Task<Group> AddGroupPermissionToDoorAsync(Guid id, Guid groupId)
        {
            var GroupPermission = _context.GroupPermissions.FirstOrDefault(gp => gp.GroupId == groupId);

            if (GroupPermission == null)
            {
                var newGroupPermission = new GroupPermission
                {
                    DoorId = id,
                    GroupId = groupId
                };

                _context.GroupPermissions.Add(newGroupPermission);
            }

            await _context.SaveChangesAsync();

            // find group and return it
            return await _context.Groups.FirstOrDefaultAsync(g => g.Id == groupId);
        }

        public async Task<User> AddUserPermissionToDoor(Guid id, Guid userId)
        {
            var UserPermission = _context.UserPermissions.FirstOrDefault(gp => gp.UserId == userId);

            if (UserPermission == null)
            {
                var newUserPermission = new UserPermission
                {
                    DoorId = id,
                    UserId = userId
                };

                _context.UserPermissions.Add(newUserPermission);
            }

            await _context.SaveChangesAsync();

            // find group and return it
            return await _context.Users.FirstOrDefaultAsync(g => g.Id == userId);
        }

        public async Task RemoveGroupPermissionFromDoorAsync(Guid id, Guid groupId)
        {
            var groupPermission = _context.GroupPermissions.FirstOrDefault(gp => gp.DoorId == id && gp.GroupId == groupId);

            if (groupPermission != null)
            {
                _context.GroupPermissions.Remove(groupPermission);
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveUserPermissionFromDoorAsync(Guid id, Guid userId)
        {
            var userPermission = _context.UserPermissions.FirstOrDefault(gp => gp.DoorId == id && gp.UserId == userId);

            if (userPermission != null)
            {
                _context.UserPermissions.Remove(userPermission);
                await _context.SaveChangesAsync();
            }
        }
    }
}
