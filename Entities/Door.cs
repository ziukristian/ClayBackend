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
        public IList<UserPermission> UserPermissions { get; set; } = [];
        public IList<GroupPermission> GroupPermission { get; set; } = [];
        public IList<ActivityLog> ActivityLogs { get; set; } = [];
    }
}
