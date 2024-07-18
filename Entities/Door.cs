using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClayBackend.Entities
{
    public class Door
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid? LockId { get; set; } = null;
        [Required]
        [MaxLength(200)]
        public required string Description { get; set; }
        public bool IsOpen { get; set; } = false;
        public IEnumerable<UserPermission> UserPermissions { get; set; } = [];
        public IEnumerable<GroupPermission> GroupPermission { get; set; } = [];
        public IEnumerable<ActivityLog> ActivityLogs { get; set; } = [];
    }
}
