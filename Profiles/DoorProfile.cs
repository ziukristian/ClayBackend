using AutoMapper;
using ClayBackend.Entities;
using ClayBackend.Models.Doors;

namespace ClayBackend.Profiles
{
    public class DoorProfile : Profile
    {
        public DoorProfile()
        {
            CreateMap<Door, DoorReadShallowDTO>();
            CreateMap<Door, DoorReadDetailsDTO>();
            CreateMap<DoorUpdateDTO, Door>();
            CreateMap<DoorInsertDTO, Door>();
        }
    }
}
