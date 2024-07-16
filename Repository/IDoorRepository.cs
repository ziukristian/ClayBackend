using ClayBackend.Models;

namespace ClayBackend.Repository
{
    public interface IDoorRepository
    {
        public List<Door> GetAllDoors();
        public List<Door> GetAccessibleDoors();
        public Door AddDoor(Door door);
        public void ChangeAccessLevel(Guid id, int newAccessLevel);
    }
}
