using AutoMapper;
using SQE.Data;
using SQE.Models;

namespace SQE.Configuration
{
    public class MapperInitilizer : Profile
    {
        public MapperInitilizer()
        {
            CreateMap<ApiUser, UserDOT>().ReverseMap();
        }
    }
}
