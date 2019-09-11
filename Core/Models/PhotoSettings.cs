using System.IO;
using System.Linq;

namespace Vegas.Core.Models
{
    public class PhotoSettings
    {
        public int MaxBytes { get; set; }
        public string[] ExtensionsAllowed { get; set; }
        public bool IsValid(string fileName){
            return ExtensionsAllowed.Any(x => x == Path.GetExtension(fileName).ToLower());
        }
    }
}