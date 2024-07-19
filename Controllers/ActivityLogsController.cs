using AutoMapper;
using ClayBackend.Models.ActivityLogs;
using ClayBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;

namespace ClayBackend.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/v{version:apiVersion}/activitylogs")]
    [ApiVersion("1.0")]
    public class ActivityLogsController (IMapper mapper, IHttpContextAccessor httpContextAccessor, IActivityLoggerService activityLoggerService) : ControllerBase
    {
        private readonly IMapper _mapper = mapper;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly IActivityLoggerService _activityLoggerService = activityLoggerService;


        [HttpGet]
        public async Task<ActionResult<IList<ActivityLogReadDTO>>> GetActivityLogs(int pageNumber = 1, int pageSize = 30)
        {
            var userId = Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (!await _activityLoggerService.CanAccessLogs(userId))
            {
                return Forbid();
            }

            var (activityLogs, paginationData) = await _activityLoggerService.GetActivityLogsAsync(pageNumber, pageSize);

            Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(paginationData));

            return Ok(_mapper.Map<IList<ActivityLogReadDTO>>(activityLogs));
        }

        
    }
}
