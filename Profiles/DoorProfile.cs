using AutoMapper;
using ClayBackend.Entities;
using ClayBackend.Models;
using ClayBackend.Services;

namespace ClayBackend.Profiles
{
    public class DoorProfile : Profile
    {
        public DoorProfile()
        {
            CreateMap<Entities.Door, Models.DoorReadDTO>();
        }
    }
}
