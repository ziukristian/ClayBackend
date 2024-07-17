using AutoMapper;
using ClayBackend.Models;
using ClayBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ClayBackend.Controllers
{
    [ApiController]
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/doors")]
    public class DoorsController(IDoorRepository doorRepository, IMapper mapper) : ControllerBase
    {
        private readonly IDoorRepository _doorRepository = doorRepository;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DoorReadDTO>>> GetDoors(int pageNumber = 1, int pageSize = 10)
        {
            var (doors, paginationData) = await _doorRepository.GetDoorsAsync(pageNumber, pageSize);

            Response.Headers.Append("X-Pagination",JsonSerializer.Serialize(paginationData));

            return Ok(_mapper.Map<IEnumerable<DoorReadDTO>>(doors));
        }


    }
}
