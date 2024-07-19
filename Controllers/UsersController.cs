using AutoMapper;
using ClayBackend.Context;
using ClayBackend.Entities;
using ClayBackend.Models.Groups;
using ClayBackend.Models.Users;
using ClayBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace ClayBackend.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/v{version:apiVersion}/users")]
    [ApiVersion("1.0")]
    public class UsersController(IMapper mapper, UserManager<User> userManager) : Controller
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        public async Task<ActionResult<IList<UserReadShallowDTO>>> GetUsers(int pageNumber = 1, int pageSize = 10)
        {
            var collection = _userManager.Users;

            var itemCount = await collection.CountAsync();

            var paginationData = new PaginationData(itemCount, pageSize, pageNumber);

            Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(paginationData));

            var users = await collection
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return Ok(_mapper.Map<IList<UserReadShallowDTO>>(users));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<UserReadDetailsDTO>> GetUser(Guid id)
        {
            var user = await _userManager.Users
                .Include(u => u.Groups)
                .Include(u => u.Permissions)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<UserReadDetailsDTO>(user));
        }

        [HttpPost]
        public async Task<ActionResult> AddRoleToUser(string role)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.AddToRoleAsync(user, role);

            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }


    }
}
