using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClayBackend.Entities
{
    public class GroupMembership
    {
        [Required]
        public Guid GroupId { get; set; }
        public Group Group { get; set; } = null!;
        [Required]
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
