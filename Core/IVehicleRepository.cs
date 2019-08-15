using System.Collections.Generic;
using System.Threading.Tasks;
using Vegas.Core.Models;

namespace Vegas.Core
{
    public interface IVehicleRepository
    {
        Task<IEnumerable<Make>> GetMakesAsync();
        Task<IEnumerable<Feature>> GetFeaturesAsync();
        Task<Vehicle> GetVehicleAsync(int id, bool includeRelatedObjects = true);
        void Add(Vehicle vehicle);
        void Remove(Vehicle vehicle);
    }
}