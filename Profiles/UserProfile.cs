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
            // User to UserReadShallowDTO
            CreateMap<User, UserReadShallowDTO>();

            // Mapping from GroupMembership to GroupReadShallowDTO
            CreateMap<GroupMembership, GroupReadShallowDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Group.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Group.Name));

            // User to UserReadDetailsDTO
            CreateMap<User, UserReadDetailsDTO>()
                .ForMember(dest => dest.GroupMemberships, opt => opt.MapFrom(src => src.GroupMemberships))
                .ForMember(dest => dest.PermittedDoors, opt => opt.MapFrom(src => src.Permissions));

            // UserPermission to UserPermissionReadDTO
            CreateMap<UserPermission, UserPermissionReadDTO>();

            // UserPermission to DoorReadShallowDTO
            CreateMap<UserPermission, DoorReadShallowDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.DoorId))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Door.Description))
                .ForMember(dest => dest.IsOpen, opt => opt.MapFrom(src => src.Door.IsOpen));

        }
    }
}
