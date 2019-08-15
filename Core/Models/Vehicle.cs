using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vegas.Core.Models
{
    [Table("Vehicles")]
    public class Vehicle
    {
        public Vehicle() 
        {
            this.VehicleFeatures = new Collection<VehicleFeature>();
        }
        public int Id { get; set; }
        [Required]
        public Model Model { get; set; }
        public int ModelId { get; set; }
        public bool IsRegistered { get; set; }
        [Required]
        [MaxLength(255)]
        public string ContactName  { get; set; }
        [Required]
        [MaxLength(255)]
        public string ContactPhone { get; set; }
        [MaxLength(255)]
        public string ContactEmail { get; set; }
        public ICollection<VehicleFeature> VehicleFeatures { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}