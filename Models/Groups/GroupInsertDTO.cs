namespace ClayBackend.Models.Groups
{
    public class GroupInsertDTO
    {
        public required string Name { get; set; }
        public bool CanMonitor { get; set; } = false;
    }
}
