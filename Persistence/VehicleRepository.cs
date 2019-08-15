using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vegas.Core;
using Vegas.Core.Models;

namespace Vegas.Persistence
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly VegaDbContext context;

        public VehicleRepository(VegaDbContext context)
        {
            this.context = context;
        }

        public void Add(Vehicle vehicle)
        {
            context.Vehicles.Add(vehicle);
        }

        public async Task<IEnumerable<Feature>> GetFeaturesAsync()
        {
            return await context.Features.ToListAsync();
        }
        public async Task<IEnumerable<Make>> GetMakesAsync()
        {
            return await context.Makes.Include(m => m.Models).ToListAsync();
        }

        public async Task<Vehicle> GetVehicleAsync(int id, bool includeRelatedObjects = true)
        {
            if(!includeRelatedObjects){
                return await context.Vehicles.FindAsync(id);
            }
            return await context.Vehicles
            .Include(x=>x.VehicleFeatures)
                .ThenInclude(vr => vr.Feature)
            .Include(vr => vr.Model)
                .ThenInclude(vr => vr.Make)
            .FirstOrDefaultAsync(x=>x.Id.Equals(id));
        }

        public void Remove(Vehicle vehicle)
        {
            context.Vehicles.Remove(vehicle);
        }
    }
}