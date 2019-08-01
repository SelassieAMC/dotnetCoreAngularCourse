using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Vegas.Controllers.Resources
{
    public class ModelResource
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<FeatureResource> Features { get; set; }
        public ModelResource()
        {
            Features = new Collection<FeatureResource>();
        }
    }
}