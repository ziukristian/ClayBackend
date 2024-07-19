using System.ComponentModel.DataAnnotations;

namespace ClayBackend.Models.Users
{
    public class UserPermissionReadDTO
    {
        public Guid DoorId { get; set; }
        public Guid UserId { get; set; }
    }
}
