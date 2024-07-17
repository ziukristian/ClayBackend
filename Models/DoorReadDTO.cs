namespace ClayBackend.Models
{
    public class DoorReadDTO
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public bool IsOpen { get; set; }
    }
}
