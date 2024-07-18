using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClayBackend.Entities
{
    public class ActivityLog
    {
        private DateTimeOffset timeStamp = DateTimeOffset.Now;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public Guid DoorId { get; set; }
        public required Door Door { get; set; }
        
        [Required]
        public Guid UserId { get; set; }
        public required User User { get; set; }

        public required DateTimeOffset TimeStamp { get => timeStamp; set => timeStamp = value; }
        public required string Action { get; set; }
    }
}
