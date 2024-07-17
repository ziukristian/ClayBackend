using ClayBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClayBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionsController(IDoorRepository permissionsRepository) : ControllerBase
    {
        private readonly IDoorRepository _permissionsRepository = permissionsRepository;
    }
}
