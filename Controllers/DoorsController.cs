using AutoMapper;
using ClayBackend.Entities;
using ClayBackend.Models.Doors;
using ClayBackend.Models.Groups;
using ClayBackend.Models.Users;
using ClayBackend.Services;
using ClayBackend.Services.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;

namespace ClayBackend.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/v{version:apiVersion}/doors")]
    [ApiVersion("1.0")]
    public class DoorsController(IDoorRepository doorRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor, IActivityLoggerService activityLoggerService) : ControllerBase
    {
        private readonly IDoorRepository _doorRepository = doorRepository;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly IMapper _mapper = mapper;
        private readonly IActivityLoggerService _activityLoggerService = activityLoggerService;

        [HttpGet]
        public async Task<ActionResult<IList<DoorReadShallowDTO>>> GetDoors(int pageNumber = 1, int pageSize = 10)
        {
            var (doors, paginationData) = await _doorRepository.GetDoorsAsync(pageNumber, pageSize);

            Response.Headers.Append("X-Pagination",JsonSerializer.Serialize(paginationData));

            return Ok(_mapper.Map<IList<DoorReadShallowDTO>>(doors));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<DoorReadDetailsDTO>> GetDoor(Guid id)
        {
            var door = await _doorRepository.GetDoorAsync(id);

            if (door == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<DoorReadDetailsDTO>(door));
        }

        [HttpPost]
        public async Task<ActionResult<DoorReadShallowDTO>> AddDoor(DoorInsertDTO newDoor)
        {
            // Map the DoorInsertDTO to a Door
            var mappedDoor = _mapper.Map<Door>(newDoor);

            var door = await _doorRepository.AddDoorAsync(mappedDoor);

            return CreatedAtAction(nameof(GetDoor), new { id = door.Id }, _mapper.Map<DoorReadShallowDTO>(door));
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> UpdateDoor(Guid id, DoorUpdateDTO updatedDoor)
        {
            var door = await _doorRepository.GetDoorAsync(id);

            if (door == null)
            {
                return NotFound();
            }

            _mapper.Map(updatedDoor, door);

            await _doorRepository.UpdateDoorAsync(door);

            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> RemoveDoor(Guid id)
        {
            await _doorRepository.RemoveDoorByIdAsync(id);

            return NoContent();
        }

        [HttpPost]
        [Route("{id}/open")]
        public async Task<ActionResult> OpenDoor(Guid id)
        {
           
            var userId = Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await _activityLoggerService.LogActivityAsync(id, userId, DoorActivityTypes.OpenRequest);

            var opened = await _doorRepository.OpenDoorAsync(id, userId);

            if (!opened)
            {
                return BadRequest("Door could not be opened");
            }

            return Ok();
        }

        [HttpPost]
        [Route("{id}/close")]
        public async Task<ActionResult> CloseDoor(Guid id)
        {
            var userId = Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await _doorRepository.CloseDoorAsync(id, userId);

            return Ok();
        }

        [HttpPost]
        [Route("{id}/permissions/groups")]
        public async Task<ActionResult<GroupMemberReadDTO>> AddGroupPermission(Guid id, Guid groupId)
        {
            var group = await _doorRepository.AddGroupPermissionToDoorAsync(id, groupId);
            return Ok(_mapper.Map<GroupMemberReadDTO>(group));
        }

        [HttpPost]
        [Route("{id}/permissions/users")]
        public async Task<ActionResult<GroupMemberReadDTO>> AddUserPermission(Guid id, Guid userId)
        {
            var user = await _doorRepository.AddUserPermissionToDoor(id, userId);
            return Ok(_mapper.Map<UserReadShallowDTO>(user));
        }

        [HttpDelete]
        [Route("{id}/permissions/groups")]
        public async Task<ActionResult> RemoveGroupPermission(Guid id, Guid groupId)
        {
            await _doorRepository.RemoveGroupPermissionFromDoorAsync(id, groupId);
            return NoContent();
        }

        [HttpDelete]
        [Route("{id}/permissions/users")]
        public async Task<ActionResult> RemoveUserPermission(Guid id, Guid userId)
        {
            await _doorRepository.RemoveUserPermissionFromDoorAsync(id, userId);
            return NoContent();
        }


    }
}
