using AutoMapper;
using ClayBackend.Context;
using ClayBackend.Entities;
using ClayBackend.Models.GroupPermissions;
using ClayBackend.Models.Groups;
using ClayBackend.Models.Users;
using ClayBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace ClayBackend.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/v{version:apiVersion}/groups")]
    [ApiVersion("1.0")]
    public class GroupsController(AppDbContext context, IMapper mapper) : Controller
    {
        private readonly AppDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        public async Task<ActionResult<IList<GroupMemberReadDTO>>> GetGroups(int pageNumber = 1, int pageSize = 10)
        {
            var collection = _context.Groups as IQueryable<Group>;

            var itemCount = await collection.CountAsync();

            var paginationData = new PaginationData(itemCount, pageSize, pageNumber);

            Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(paginationData));

            var groups = await collection
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return Ok(_mapper.Map<IList<GroupMemberReadDTO>>(groups));

        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<GroupReadDetailsDTO>> GetGroup(Guid id)
        {
            var group = await _context.Groups
                .Include(g => g.Members)
                .Include(g => g.Permissions)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (group == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<GroupReadDetailsDTO>(group));
        }

        [HttpPost]
        public async Task<ActionResult<GroupMemberReadDTO>> AddGroup(GroupInsertDTO newGroup)
        {
            // Map the GroupInsertDTO to a Group
            var mappedGroup = _mapper.Map<Group>(newGroup);

            _context.Groups.Add(mappedGroup);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetGroup), new { id = mappedGroup.Id }, _mapper.Map<GroupMemberReadDTO>(mappedGroup));
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> UpdateGroup(Guid id, GroupUpdateDTO updatedGroup)
        {
            var group = await _context.Groups.FirstOrDefaultAsync(g => g.Id == id);

            if (group == null)
            {
                return NotFound();
            }

            _mapper.Map(updatedGroup, group);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> RemoveGroup(Guid id)
        {
            var group = await _context.Groups.FirstOrDefaultAsync(g => g.Id == id);

            if (group != null)
            {
                _context.Groups.Remove(group);
                await _context.SaveChangesAsync();
            }

            return NoContent();
        }

        [HttpPost]
        [Route("{id}/adduser")]
        public async Task<ActionResult<UserReadShallowDTO>> AddUserToGroup(Guid id, Guid userId)
        {
            var group = await _context.Groups.FirstOrDefaultAsync(g => g.Id == id);
            if (group == null)
            {
                return NotFound("Group not recognized");
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                return NotFound("User not recognized");
            }

            var existingGroupMember = await _context.GroupMemberships.FirstOrDefaultAsync(gm => gm.GroupId == id && gm.UserId == userId);
            if (existingGroupMember == null)
            {
                var groupMember = new GroupMembership
                {
                    GroupId = id,
                    UserId = userId
                };

                _context.GroupMemberships.Add(groupMember);
                await _context.SaveChangesAsync();
            }

            return Ok(_mapper.Map<UserReadShallowDTO>(user));
        }


        // Need a call to remove a user from a group
        [HttpDelete]
        [Route("{id}/removeuser")]
        public async Task<ActionResult> RemoveUserFromGroup(Guid id, Guid userId)
        {
            var groupMember = await _context.GroupMemberships.FirstOrDefaultAsync(gm => gm.GroupId == id && gm.UserId == userId);

            if (groupMember != null)
            {
                _context.GroupMemberships.Remove(groupMember);
                await _context.SaveChangesAsync();
            }

            return NoContent();
        }
    }
}
