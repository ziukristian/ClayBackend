using ClayBackend.Context;
using ClayBackend.Models;
using System.Security.Claims;

namespace ClayBackend.Repository
{
    public class DoorRepository : IDoorRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppDbContext _context;

        public DoorRepository(IHttpContextAccessor httpContextAccessor, AppDbContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        public Door AddDoor(Door door)
        {
            _context.Doors.Add(door);
            _context.SaveChanges();
            return door;
        }

        public List<Door> GetAllDoors()
        {
            return _context.Doors.ToList();
        }

        public List<Door> GetAccessibleDoors()
        {
            var user = _httpContextAccessor.HttpContext.User;
            var roles = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();

            // Get highest AccessLevel from roles
            var accessLevel = roles.Select(r => _context.Roles.FirstOrDefault(r => r.Name == r.Name).AccessLevel).Max();

            // Get user access level
            var userAccessLevel = _context.Users.FirstOrDefault(roles => roles.Email == user.Identity.Name).AccessLevel;

            var maxAccessLevel = accessLevel > userAccessLevel ? accessLevel : userAccessLevel;

            return _context.Doors.Where(d => d.AccessLevel <= maxAccessLevel).ToList();
        }

        public void ChangeAccessLevel(Guid id, int newAccessLevel)
        {
            // Change access level of door
            Door door = _context.Doors.FirstOrDefault(d => d.Id == id);

            if (door != null)
            {
                door.AccessLevel = newAccessLevel;
                _context.SaveChanges();
            }

        }
    }
}
