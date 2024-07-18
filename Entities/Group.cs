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
        public IEnumerable<GroupMember> Members { get; set; } = [];
        public IEnumerable<GroupPermission> Permissions { get; set; } = [];
    }
}
