using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClayBackend.Entities
{
    public class ActivityLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public Guid DoorId { get; set; }
        public Door Door { get; set; } = null!;

        [Required]
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        public DateTime TimeStamp { get ; set; } =  DateTime.Now.ToUniversalTime();
        public required string Action { get; set; }
    }
}
