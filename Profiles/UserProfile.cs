using AutoMapper;
using ClayBackend.Entities;
using ClayBackend.Models.Doors;
using ClayBackend.Models.Groups;
using ClayBackend.Models.Users;

namespace ClayBackend.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserReadShallowDTO>();

            CreateMap<GroupMembership, GroupReadShallowDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Group.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Group.Name));

            CreateMap<User, UserReadDetailsDTO>()
                .ForMember(dest => dest.GroupMemberships, opt => opt.MapFrom(src => src.GroupMemberships))
                .ForMember(dest => dest.PermittedDoors, opt => opt.MapFrom(src => src.Permissions))
                .ForMember(dest => dest.ActivityLogs, opt => opt.MapFrom(src => src.ActivityLogs));

            CreateMap<UserPermission, UserPermissionReadDTO>();

            CreateMap<UserPermission, DoorReadShallowDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.DoorId))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Door.Description))
                .ForMember(dest => dest.IsOpen, opt => opt.MapFrom(src => src.Door.IsOpen));

        }
    }
}
