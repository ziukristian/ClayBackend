using ClayBackend.Models.ActivityLogs;
using ClayBackend.Models.Doors;
using ClayBackend.Models.Groups;

namespace ClayBackend.Models.Users
{
    public class UserReadDetailsDTO
    {
        public Guid Id { get; set; }
        public string? UserName { get; set; }
        public IList<GroupReadShallowDTO> GroupMemberships { get; set; } = [];
        public IList<DoorReadShallowDTO> PermittedDoors { get; set; } = [];
        public IList<ActivityLogReadDTO> ActivityLogs { get; set; } = [];

    }
}
