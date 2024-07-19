using ClayBackend.Entities;
using ClayBackend.Models.Doors;
using ClayBackend.Models.Groups;
using ClayBackend.Models.UserPermissions;

namespace ClayBackend.Models.Users
{
    public class UserReadDetailsDTO
    {
        public Guid Id { get; set; }
        public string? UserName { get; set; }
        public IList<GroupReadShallowDTO> GroupMemberships { get; set; } = [];
        public IList<DoorReadShallowDTO> PermittedDoors { get; set; } = [];

    }
}
