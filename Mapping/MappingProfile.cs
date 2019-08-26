using System.Linq;
using AutoMapper;
using Vegas.Controllers.Resources;
using Vegas.Core.Models;

namespace Vegas.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Domain model to API Resource
            CreateMap<VehicleQuery,VehicleQueryResource>();
            CreateMap<Make,MakeResource>();
            CreateMap<Model,KeyValuePairResource>();
            CreateMap<Feature, KeyValuePairResource>();
            CreateMap<Vehicle,SaveVehicleResource>()
                .ForMember(vr => vr.Contact, opt => 
                    opt.MapFrom(v => new ContactResource {Name = v.ContactName, Phone = v.ContactPhone, Email = v.ContactEmail}))
                .ForMember(vr => vr.Features, opt => opt.MapFrom(src => src.VehicleFeatures.Select( vf => vf.FeatureId)));
                
            CreateMap<Vehicle,VehicleResource>()
                .ForMember(vr => vr.Contact, opt => 
                    opt.MapFrom(v => new ContactResource {Name = v.ContactName, Phone = v.ContactPhone, Email = v.ContactEmail}))
                .ForMember(vr => vr.Make, opt => opt.MapFrom(v => new KeyValuePairResource {Id = v.Model.Make.Id, Name = v.Model.Make.Name}))
                .ForMember(vr => vr.Features, opt => 
                    opt.MapFrom(v => v.VehicleFeatures.Select( vr => new KeyValuePairResource{Id = vr.Feature.Id, Name = vr.Feature.Name})));
                  
            //API Resouce to Domain
            CreateMap<VehicleQueryResource,VehicleQuery>();
            CreateMap<SaveVehicleResource,Vehicle>()
                .ForMember(vr => vr.Id, opt => opt.Ignore())
                .ForMember(v => v.ContactName,opt => opt.MapFrom(src => src.Contact.Name))
                .ForMember(v => v.ContactPhone,opt => opt.MapFrom(src => src.Contact.Phone))
                .ForMember(v => v.ContactEmail,opt => opt.MapFrom(src => src.Contact.Email))
                .ForMember(v => v.VehicleFeatures,opt => opt.Ignore())
                .AfterMap((vr , v) => {
                    //Remove unselected features
                    var removedFeatures = v.VehicleFeatures.Where(f => !vr.Features.Contains(f.FeatureId));
                    foreach(var f in removedFeatures.ToList()){
                            v.VehicleFeatures.Remove(f);
                    }
                            
                    //addind selected features
                    var addedFeatures = vr.Features
                        .Where(id =>!v.VehicleFeatures.Any(x=>x.FeatureId == id))
                        .Select(id => new VehicleFeature{FeatureId = id});
                    foreach(var f in addedFeatures.ToList()){
                        v.VehicleFeatures.Add(f);
                    }
                });

            CreateMap<KeyValuePairResource, Feature>();
            CreateMap<KeyValuePairResource, Model>();
            CreateMap<KeyValuePairResource, Make>();
            CreateMap<VehicleFeature, VehicleFeatureResource>();
            CreateMap<VehicleFeatureResource, VehicleFeature>();
             

        }
    }
}