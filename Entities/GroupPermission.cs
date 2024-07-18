using System.ComponentModel.DataAnnotations;

namespace ClayBackend.Entities
{
    public class GroupPermission
    {
        [Required]
        public Guid DoorId { get; set; }
        public Door Door { get; set; } = null!;
        [Required]
        public Guid GroupId { get; set; }
        public Group Group { get; set; } = null!;
    }
}
