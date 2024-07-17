namespace ClayBackend.Entities
{
    public class DoorPermission
    {
        public Guid DoorId { get; set; }
        public Guid AuthorizedEntityId { get; set; }
    }
}
