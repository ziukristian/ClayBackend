using ClayBackend.Context;
using ClayBackend.Entities;
using ClayBackend.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace ClayBackend.Services.Repos
{
    public class DoorRepository(IHttpContextAccessor httpContextAccessor, AppDbContext context) : IDoorRepository
    {
        //private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
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

        public Task<Door> AddDoorAsync(Door newDoor)
        {
            _context.Doors.Add(newDoor);
            _context.SaveChanges();

            return Task.FromResult(newDoor);
        }

        public Task<Door> GetDoorAsync(Guid id)
        {
            // Return door with all permissions
            // .Include(d => d.Permissions)
            return _context.Doors.FirstOrDefaultAsync(d => d.Id == id);

        }

        public Task<bool> DoorExistsAsync(Guid id)
        {
            return _context.Doors.AnyAsync(d => d.Id == id);
        }

        public Task<Door> UpdateDoorAsync(Door door)
        {
            _context.Doors.Update(door);
            _context.SaveChanges();

            return Task.FromResult(door);
        }

        public Task<bool> RemoveDoorAsync(Guid id)
        {
            var door = _context.Doors.FirstOrDefault(d => d.Id == id);

            if (door == null)
            {
                return Task.FromResult(false);
            }

            _context.Doors.Remove(door);
            _context.SaveChanges();

            return Task.FromResult(true);
        }

        void IDoorRepository.RemoveDoorAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> OpenDoorAsync(Guid id, Guid userid)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> CloseDoorAsync(Guid id, Guid userid)
        {
            throw new NotImplementedException();
        }

        public Task<Group> AddGroupPermissionToDoor(Guid id, Guid groupId)
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

            // find group and return it
            return _context.Groups.FirstOrDefaultAsync(g => g.Id == groupId);
        }

        public Task<User> AddUserPermissionToDoor(Guid id, Guid userId)
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

            // find group and return it
            return _context.Users.FirstOrDefaultAsync(g => g.Id == userId);
        }
    }
}
