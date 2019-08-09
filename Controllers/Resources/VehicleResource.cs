using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Vegas.Controllers.Resources
{
    public class VehicleResource
    {
        public int Id { get; set; }
        public int MakeId { get; set; }
        [Required]
        public int ModelId { get; set; }
        public bool IsRegistered { get; set; }
        public ICollection<VehicleFeatureResource> VehicleFeatures  { get; set; }
        [Required]
        [MaxLength(255)]
        public string ContactName  { get; set; }
        [Required]
        [MaxLength(255)]
        public string ContactPhone { get; set; }
        [MaxLength(255)]
        public string ContactEmail { get; set; }
    }
}