using ClayBackend.Models;
using ClayBackend.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClayBackend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DoorController(IDoorRepository doorRepository) : ControllerBase
    {
        private readonly IDoorRepository _doorRepository = doorRepository;

        [HttpGet]
        [Authorize]
        public IActionResult GetAllDoors()
        {
            return Ok(_doorRepository.GetAllDoors());
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAccessibleDoors()
        {
            return Ok(_doorRepository.GetAccessibleDoors());
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddDoor(Door door)
        {
            return Ok(_doorRepository.AddDoor(door));
        }

        [HttpPatch]
        [Authorize(Roles = "Admin")]
        [Route("{id:guid}")]
        public IActionResult ChangeAccessLevel(Guid id, [FromBody] int newAccessLevel)
        {
            _doorRepository.ChangeAccessLevel(id, newAccessLevel);
            return Ok();
        }
    }
}
