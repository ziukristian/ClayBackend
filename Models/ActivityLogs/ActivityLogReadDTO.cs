using ClayBackend.Entities;

namespace ClayBackend.Models.ActivityLogs
{
    public class ActivityLogReadDTO
    {
        public Door Door { get; set; }
        public User User { get; set; } 
        public DateTime TimeStamp { get; set; }
        public required string Action { get; set; }
    }
}
