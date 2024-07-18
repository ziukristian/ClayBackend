using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClayBackend.Entities
{
    public class GroupMember
    {
        [Required]
        public Guid GroupId { get; set; }
        public required Group Group { get; set; }
        [Required]
        public Guid UserId { get; set; }
        public required User User { get; set; }
    }
}
