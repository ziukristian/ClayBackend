using AutoMapper;
using ClayBackend.Entities;
using ClayBackend.Models.ActivityLogs;
using ClayBackend.Models.Doors;

namespace ClayBackend.Profiles
{
    public class ActivityLogProfile : Profile
    {
        public ActivityLogProfile()
        {
            CreateMap<ActivityLog, ActivityLogReadDTO>();
        }
    }
}
