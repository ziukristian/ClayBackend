using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClayBackend.Entities
{
    public class Group
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        public required string Name { get; set; }
        public IList<GroupMembership> Memberships { get; set; } = [];
        public IList<GroupPermission> Permissions { get; set; } = [];
        public IList<User> Members { get; set; } = [];
        public bool CanMonitor { get; set; } = false;
    }
}
