using AutoMapper;
using ClayBackend.Context;
using ClayBackend.Entities;
using ClayBackend.Models.Doors;
using ClayBackend.Models.Groups;
using ClayBackend.Models.Users;
using ClayBackend.Repositories;
using ClayBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace ClayBackend.Controllers
{
    [ApiController]
    [Authorize(Roles = "Admin")]
    [Route("api/v{version:apiVersion}/groups")]
    [ApiVersion("1.0")]
    public class GroupsController(IMapper mapper, IGroupRepository groupRepository, IUserRepository userRepository) : Controller
    {
        private readonly IMapper _mapper = mapper;
        private readonly IGroupRepository _groupRepository = groupRepository;
        private readonly IUserRepository _userRepository = userRepository; 

        [HttpGet]
        public async Task<ActionResult<IList<GroupMemberReadDTO>>> GetGroups(int pageNumber = 1, int pageSize = 10)
        {
            var (groups, paginationData) = await _groupRepository.GetGroupsAsync(pageNumber, pageSize);

            Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(paginationData));

            return Ok(_mapper.Map<IList<GroupMemberReadDTO>>(groups));

        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<GroupReadDetailsDTO>> GetGroup(Guid id)
        {
            var group = await _groupRepository.GetGroupByIdAsync(id);

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

            var group = await _groupRepository.AddGroupAsync(mappedGroup);

            return CreatedAtAction(nameof(GetGroup), new { id = group.Id }, _mapper.Map<GroupMemberReadDTO>(group));
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> UpdateGroup(Guid id, GroupUpdateDTO updatedGroup)
        {
            var group = await _groupRepository.GetGroupByIdAsync(id);

            if (group == null)
            {
                return NotFound();
            }

            _mapper.Map(updatedGroup, group);

            await _groupRepository.UpdateGroupAsync(group);

            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> RemoveGroup(Guid id)
        {
            await _groupRepository.RemoveGroupByIdAsync(id);

            return NoContent();
        }

        [HttpPost]
        [Route("{id}/adduser")]
        public async Task<ActionResult<UserReadShallowDTO>> AddUserToGroup(Guid id, Guid userId)
        {
            if (!await _groupRepository.GroupExistsAsync(id))
            {
                return NotFound("Group not recognized");
            }

            if (!await _userRepository.UserExistsAsync(userId))
            {
                return NotFound("User not recognized");
            }

            if (!await _groupRepository.HasMembershipAsync(userId,id))
            {
                var groupMember = new GroupMembership
                {
                    GroupId = id,
                    UserId = userId
                };

                await _groupRepository.AddMembershipAsync(groupMember);
            }

            return Ok();
        }


        // Need a call to remove a user from a group
        [HttpDelete]
        [Route("{id}/removeuser")]
        public async Task<ActionResult> RemoveUserFromGroup(Guid id, Guid userId)
        {
            await _groupRepository.RevokeMembershipAsync(id, userId);

            return NoContent();
        }
    }
}
