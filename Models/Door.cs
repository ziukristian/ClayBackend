namespace ClayBackend.Models
{
    public class Door
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required Guid LockId { get; set; }
        public required int AccessLevel { get; set; } = 0;
        public required string Name { get; set; }
        public bool IsOpen { get; set; } = false;

    }
}
