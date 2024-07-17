using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClayBackend.Entities
{
    public class DoorActivityLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [ForeignKey("DoorId")]
        public Door Door { get; set; }
        public Guid DoorId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
        public string UserId { get; set; }

        public DateTimeOffset TimeStamp { get; set; } = DateTimeOffset.Now;
        public required int ActionCode { get; set; }
    }
}
