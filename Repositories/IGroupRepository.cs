using ClayBackend.Entities;
using ClayBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClayBackend.Repositories
{
    public interface IGroupRepository
    {
        Task<(IList<Group>, PaginationData)> GetGroupsAsync(int pageNumber, int pageSize);
        Task<Group?> GetGroupByIdAsync(Guid id);
        Task<Group> AddGroupAsync(Group group);
        Task UpdateGroupAsync(Group group);
        Task RemoveGroupByIdAsync(Guid id);
        Task RevokeMembershipAsync(Guid groupId, Guid userId);
        Task<bool> GroupExistsAsync(Guid groupId);
        Task<bool> HasMembershipAsync(Guid userId, Guid groupId );
        Task<GroupMembership> AddMembershipAsync(GroupMembership membership);

    }
}
