using AutoMapper;
using Vegas.Controllers.Resources;
using Vegas.Models;

namespace Vegas.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Make,MakeResource>();
            CreateMap<Model,ModelResource>();
            CreateMap<Feature,FeatureResource>();
        }
    }
}