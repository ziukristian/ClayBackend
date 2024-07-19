using AutoMapper;
using ClayBackend.Context;
using ClayBackend.Entities;
using ClayBackend.Models.Groups;
using ClayBackend.Models.Users;
using ClayBackend.Repositories;
using ClayBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace ClayBackend.Controllers
{
    [ApiController]
    [Authorize(Roles = "Admin")]
    [Route("api/v{version:apiVersion}/users")]
    [ApiVersion("1.0")]
    public class UsersController(IMapper mapper, UserManager<User> userManager, IUserRepository userRepository) : Controller
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly IMapper _mapper = mapper;
        private readonly IUserRepository _userRepository = userRepository;

        [HttpGet]
        public async Task<ActionResult<IList<UserReadShallowDTO>>> GetUsers(int pageNumber = 1, int pageSize = 10)
        {
            var (users, paginationData) = await _userRepository.GetUsersAsync(pageNumber, pageSize);

            Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(paginationData));

            return Ok(_mapper.Map<IList<UserReadShallowDTO>>(users));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<UserReadDetailsDTO>> GetUser(Guid id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);

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
                return NotFound("");
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
