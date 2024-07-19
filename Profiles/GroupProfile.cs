using AutoMapper;
using ClayBackend.Entities;
using ClayBackend.Models.Doors;
using ClayBackend.Models.Groups;

namespace ClayBackend.Profiles
{
    public class GroupProfile : Profile
    {
        public GroupProfile()
        {
            CreateMap<Group, GroupMemberReadDTO>();
            CreateMap<Group, GroupReadDetailsDTO>()
                .ForMember(dest => dest.Members, opt => opt.MapFrom(src => src.Members))
                .ForMember(dest => dest.PermittedDoors, opt => opt.MapFrom(src => src.Permissions));
            CreateMap<GroupInsertDTO, Group>();
            CreateMap<GroupUpdateDTO, Group>();
            CreateMap<GroupMembership, GroupReadShallowDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Group.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Group.Name));
            CreateMap<GroupPermission, DoorReadShallowDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.DoorId))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Door.Description))
                .ForMember(dest => dest.IsOpen, opt => opt.MapFrom(src => src.Door.IsOpen));
        }
    }
}
