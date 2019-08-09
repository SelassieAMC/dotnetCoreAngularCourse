using System.Linq;
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
            CreateMap<Feature, FeatureResource>();
            CreateMap<FeatureResource, Feature>();
            CreateMap<Vehicle,VehicleResource>();
            CreateMap<VehicleFeature, VehicleFeatureResource>();
            CreateMap<VehicleFeatureResource, VehicleFeature>();
            CreateMap<VehicleResource,Vehicle>();  

        }
    }
}