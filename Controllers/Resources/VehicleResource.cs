using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Vegas.Controllers.Resources
{
    public class VehicleResource
    {
        public VehicleResource() 
        {
            this.Features = new Collection<KeyValuePairResource>();
        }
        public int Id { get; set; }
        [Required]
        public KeyValuePairResource Model { get; set; }
        public KeyValuePairResource Make { get; set; }
        public bool IsRegistered { get; set; }
        [Required]
        public ContactResource Contact  { get; set; }
        public ICollection<KeyValuePairResource> Features { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}