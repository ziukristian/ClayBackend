namespace ClayBackend.Models.Groups
{
    public class GroupUpdateDTO
    {
        public required string Name { get; set; }
        public bool CanMonitor { get; set; }
    }
}
