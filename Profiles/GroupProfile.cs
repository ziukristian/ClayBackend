using AutoMapper;

namespace ClayBackend.Profiles
{
    public class GroupProfile : Profile
    {
        public GroupProfile()
        {
            CreateMap<Entities.Group, Models.GroupReadShallowDTO>();
        }
    }
}
