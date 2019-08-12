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
            //Domain model to API Resource
            CreateMap<Make,MakeResource>();
            CreateMap<Model,ModelResource>();
            CreateMap<Feature, FeatureResource>();
            CreateMap<Vehicle,VehicleResource>()
                .ForMember(vr => vr.Contact, opt => 
                    opt.MapFrom(v => new ContactResource {Name = v.ContactName, Phone = v.ContactPhone, Email = v.ContactEmail}))
                .ForMember(vr => vr.Features, opt => opt.MapFrom(src => src.VehicleFeatures.Select( vf => vf.FeatureId)));
                  
            //API Resouce to Domain
            CreateMap<VehicleResource,Vehicle>()
                .ForMember(vr => vr.Id, opt => opt.Ignore())
                .ForMember(v => v.ContactName,opt => opt.MapFrom(src => src.Contact.Name))
                .ForMember(v => v.ContactPhone,opt => opt.MapFrom(src => src.Contact.Phone))
                .ForMember(v => v.ContactEmail,opt => opt.MapFrom(src => src.Contact.Email))
                .ForMember(v => v.VehicleFeatures,opt => opt.MapFrom(src => src.Features.Select( id => new VehicleFeature {FeatureId  = id})));

            CreateMap<FeatureResource, Feature>();
            
            CreateMap<VehicleFeature, VehicleFeatureResource>();
            CreateMap<VehicleFeatureResource, VehicleFeature>();
             

        }
    }
}