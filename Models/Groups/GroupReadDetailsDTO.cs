using ClayBackend.Models.Doors;
using ClayBackend.Models.UserPermissions;
using ClayBackend.Models.Users;

namespace ClayBackend.Models.Groups
{
    public class GroupReadDetailsDTO
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public IList<UserReadShallowDTO> Members { get; set; } = [];
        public IList<DoorReadShallowDTO> PermittedDoors { get; set; } = [];
    }
}
