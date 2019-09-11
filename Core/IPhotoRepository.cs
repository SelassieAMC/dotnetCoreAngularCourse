using System.Collections.Generic;
using System.Threading.Tasks;
using Vegas.Core.Models;

namespace Vegas.Core
{
    public interface IPhotoRepository
    {
        Task<IEnumerable<Photo>> GetPhotos(int ve);
    }
}