using System.ComponentModel.DataAnnotations;

namespace ClayBackend.Models.Doors
{
    public class DoorInsertDTO
    {
        [Required(ErrorMessage = "Description is a required field")]
        [MaxLength(200)]
        public required string Description { get; set; }
        public Guid? LockId { get; set; }
    }
}
