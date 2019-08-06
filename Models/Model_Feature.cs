using System.ComponentModel.DataAnnotations.Schema;

namespace Vegas.Models
{
    [Table("Model_Features")]
    public class Model_Feature
    {
        public int ModelId { get; set; }
        public Model Model { get; set; }
        public int FeatureId { get; set; }
        public Feature Feature { get; set; }
    }
}