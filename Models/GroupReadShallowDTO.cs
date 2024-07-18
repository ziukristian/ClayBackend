using System.ComponentModel.DataAnnotations;

namespace ClayBackend.Models
{
    public class GroupReadShallowDTO
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
    }
}
