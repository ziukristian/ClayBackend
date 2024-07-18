using AutoMapper;

namespace ClayBackend.Profiles
{
    public class DoorProfile : Profile
    {
        public DoorProfile()
        {
            CreateMap<Entities.Door, Models.DoorReadShallowDTO>();
            CreateMap<Entities.Door, Models.DoorReadDetailsDTO>();
            CreateMap<Models.DoorUpdateDTO, Entities.Door>();
            CreateMap<Models.DoorInsertDTO, Entities.Door>();
        }
    }
}
