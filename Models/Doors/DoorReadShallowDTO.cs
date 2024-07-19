namespace ClayBackend.Models.Doors
{
    public class DoorReadShallowDTO
    {
        public Guid Id { get; set; }
        public string? Description { get; set; }
        public bool IsOpen { get; set; }
    }
}
