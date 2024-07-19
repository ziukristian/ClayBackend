using ClayBackend.Context;
using ClayBackend.Entities;
using ClayBackend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace ClayBackend.Repositories
{
    public class GroupRepository(AppDbContext context) : IGroupRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<Group> AddGroupAsync(Group group)
        {
            _context.Groups.Add(group);
            await _context.SaveChangesAsync();
            return group;
        }

        public async Task<GroupMembership> AddMembershipAsync(GroupMembership membership)
        {
            _context.GroupMemberships.Add(membership);
            await _context.SaveChangesAsync();
            return membership;
        }

        public async Task<Group?> GetGroupByIdAsync(Guid id)
        {
            return await _context.Groups
                .Include(g => g.Members)
                .Include(g => g.Permissions)
                .AsSplitQuery()
                .FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<(IList<Group>, PaginationData)> GetGroupsAsync(int pageNumber, int pageSize)
        {
            var collection = _context.Groups as IQueryable<Group>;

            var itemCount = await collection.CountAsync();

            var paginationData = new PaginationData(itemCount, pageSize, pageNumber);

            var collectionToReturn = await collection
                .OrderBy(c => c.Name)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (collectionToReturn, paginationData);
        }

        public async Task<bool> GroupExistsAsync(Guid id)
        {
            return await _context.Groups.AnyAsync(d => d.Id == id);
        }

        public async Task<bool> HasMembershipAsync(Guid userId, Guid groupId)
        {
            return await _context.GroupMemberships.AnyAsync(gm => gm.GroupId == groupId && gm.UserId == userId);
        }

        public async Task RemoveGroupByIdAsync(Guid id)
        {
            var group = _context.Groups.FirstOrDefault(d => d.Id == id);

            if (group != null)
            {
                _context.Groups.Remove(group);
                await _context.SaveChangesAsync();
            }
        }

        public async Task RevokeMembershipAsync(Guid groupId, Guid userId)
        {
            var membership = _context.GroupMemberships.FirstOrDefault(gm => gm.GroupId == groupId && gm.UserId == userId);
            if (membership != null)
            {
                _context.GroupMemberships.Remove(membership);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateGroupAsync(Group group)
        {
            _context.Groups.Update(group);
            await _context.SaveChangesAsync();
        }

        
    }
}
