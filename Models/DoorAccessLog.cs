namespace ClayBackend.Models
{
    public class DoorAccessLog
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid DoorId { get; set; }
        public Guid UserId { get; set; }
        public DateTimeOffset TimeStamp { get; set; } = DateTimeOffset.Now;
        public int ActionCode { get; set; }
    }
}
