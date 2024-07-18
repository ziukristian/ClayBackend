using AutoMapper;
using ClayBackend.Entities;
using ClayBackend.Models;
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
    public class DoorsController(IDoorRepository doorRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor) : ControllerBase
    {
        private readonly IDoorRepository _doorRepository = doorRepository;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DoorReadShallowDTO>>> GetDoors(int pageNumber = 1, int pageSize = 10)
        {
            var (doors, paginationData) = await _doorRepository.GetDoorsAsync(pageNumber, pageSize);

            Response.Headers.Append("X-Pagination",JsonSerializer.Serialize(paginationData));

            return Ok(_mapper.Map<IEnumerable<DoorReadShallowDTO>>(doors));
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
            _doorRepository.RemoveDoorAsync(id);

            return NoContent();
        }

        [HttpPost]
        [Route("{id}/open")]
        public async Task<ActionResult> OpenDoor(Guid id)
        {
            return Ok();
        }

        [HttpPost]
        [Route("{id}/close")]
        public async Task<ActionResult> CloseDoor(Guid id)
        {
            return Ok();
        }

        [HttpPost]
        [Route("{id}/permissions/addgroup")]
        public async Task<ActionResult<GroupReadShallowDTO>> AddGroupPermission(Guid id, Guid groupId)
        {
            var group = await _doorRepository.AddGroupPermissionToDoor(id, groupId);
            return Ok(_mapper.Map<GroupReadShallowDTO>(group));
        }

        [HttpPost]
        [Route("{id}/permissions/adduser")]
        public async Task<ActionResult<GroupReadShallowDTO>> AddUserPermission(Guid id, Guid userId)
        {
            var user = await _doorRepository.AddUserPermissionToDoor(id, userId);
            return Ok(_mapper.Map<UserReadShallowDTO>(user));
        }


    }
}
