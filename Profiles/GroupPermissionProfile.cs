using AutoMapper;
using ClayBackend.Entities;
using ClayBackend.Models.Users;

namespace ClayBackend.Profiles
{
    public class GroupPermissionProfile : Profile
    {
        public GroupPermissionProfile()
        {
            CreateMap<GroupPermission, UserPermissionReadDTO>();
            CreateMap<GroupMembership, GroupPermission>();
        }
    }
}
