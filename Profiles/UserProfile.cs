using AutoMapper;

namespace ClayBackend.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<Entities.User, Models.UserReadShallowDTO>();
        }
    }
}
