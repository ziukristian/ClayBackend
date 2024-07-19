using System.ComponentModel.DataAnnotations;

namespace ClayBackend.Models.Groups
{
    public class GroupPermissionReadDTO
    {
        public Guid DoorId { get; set; }
        public Guid GroupId { get; set; }
    }
}
