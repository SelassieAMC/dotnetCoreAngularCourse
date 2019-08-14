using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vegas.Models;

namespace Vegas.Persistence
{
    public class VehicleRepository
    {
        private readonly VegaDbContext context;

        public VehicleRepository(VegaDbContext context)
        {
            this.context = context;
        }

        public async Task<Vehicle> GetVehicleAsync(int id){
            return await context.Vehicles
            .Include(x=>x.VehicleFeatures)
                .ThenInclude(vr => vr.Feature)
            .Include(vr => vr.Model)
                .ThenInclude(vr => vr.Make)
            .FirstOrDefaultAsync(x=>x.Id.Equals(id));
        }
    }
}