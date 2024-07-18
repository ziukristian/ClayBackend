using System.ComponentModel.DataAnnotations;

namespace ClayBackend.Entities
{
    public class UserPermission
    {
        [Required]
        public Guid DoorId { get; set; }
        public Door Door { get; set; } = null!;
        [Required]
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
