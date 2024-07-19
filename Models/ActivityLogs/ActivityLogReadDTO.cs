using ClayBackend.Entities;
using ClayBackend.Models.Doors;
using ClayBackend.Models.Users;

namespace ClayBackend.Models.ActivityLogs
{
    public class ActivityLogReadDTO
    {
        public DoorReadShallowDTO Door { get; set; }
        public UserReadShallowDTO User { get; set; } 
        public DateTime TimeStamp { get; set; }
        public required string Action { get; set; }
    }
}
