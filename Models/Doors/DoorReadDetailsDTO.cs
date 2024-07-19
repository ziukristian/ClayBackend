using ClayBackend.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClayBackend.Models.Doors
{
    public class DoorReadDetailsDTO
    {
        public Guid Id { get; set; }
        public Guid? LockId { get; set; } = null;
        public required string Description { get; set; }
        public bool IsOpen { get; set; } = false;
        //public List<DoorPermission> Permissions { get; set; } = [];
    }
}
