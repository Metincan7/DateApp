using Api.Data;
using Api.Entites;
using AutoMapper;

namespace Api.Helpers
{
    public class AutoMapperProfiles:Profile 
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser,MemberDto>();
            CreateMap<Photo,PhotoDto>();
        }
    }
}