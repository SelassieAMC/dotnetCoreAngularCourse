using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public async Task<IEnumerable<Vehicle>> GetVehiclesAsync(int pagination, int quantity)
        {
            return await context.Vehicles
                .Include(m=>m.Model)
                 .ThenInclude(vr => vr.Make)
                .Skip(pagination*quantity)
                .Take(quantity).ToListAsync();
        }

        public async Task<IEnumerable<Vehicle>> GetVehiclesAsync(VehicleQuery queryObj)
        {
            var query = context.Vehicles
                .Include(m=>m.Model)
                 .ThenInclude(vr => vr.Make)
                .Include(f=>f.VehicleFeatures)
                    .ThenInclude(vf =>vf.Feature)
                .AsQueryable();

            if(queryObj.MakeId.HasValue){
                query = query.Where(x=>x.Model.MakeId == queryObj.MakeId);
            }
            if(queryObj.MakeId.HasValue){
                query = query.Where(x=>x.Model.Id == queryObj.ModelId);
            }
            var columnsMap = new Dictionary<string, Expression<Func<Vehicle,object>>>() {
                ["make"] = v => v.Model.Make.Name,
                ["model"] = v => v.Model.Name,
                ["contactName"] = v => v.ContactName,
                ["id"] = v => v.Id
            };
            if(queryObj.IsSortAscending){
                query = query.OrderBy(columnsMap[queryObj.SortBy]);
            }else{
                query = query.OrderByDescending(columnsMap[queryObj.SortBy]);
            }
            return await query.ToListAsync();
        }

        public void Remove(Vehicle vehicle)
        {
            context.Vehicles.Remove(vehicle);
        }
    }
}