using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vegas.Controllers.Resources;
using Vegas.Models;
using Vegas.Persistence;
using System.Collections.ObjectModel;
using System.Linq;
using Newtonsoft.Json;

namespace Vegas.Controllers
{
    [Route("/api/[controller]/")]
    public class VehicleController : Controller
    {
        private readonly VegaDbContext context;
        private readonly IMapper mapper;
        public VehicleController(VegaDbContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;
        }
        [HttpGet("getMakes")]
        public async Task<IEnumerable<MakeResource>> GetMakes(){
            var makes = await context.Makes.Include(m=>m.Models).ToListAsync();
            return mapper.Map<List<Make>,List<MakeResource>>(makes);
        }
        
        [HttpGet("getFeatures")]
        public async Task<IEnumerable<FeatureResource>> getFeaturesAsync(){
            var features = await context.Features.ToListAsync();
            return mapper.Map<IEnumerable<Feature>,IEnumerable<FeatureResource>>(features);
        }
        
        [HttpPost("addVehicle")]
        public async Task<ActionResult> addVehicleAsync([FromBody] VehicleResource vehicleBody){
           try
           {
                Vehicle newVehicle = mapper.Map<VehicleResource,Vehicle>(vehicleBody);
                var data =  context.Vehicles.AddAsync(newVehicle);
                //await context.SaveChangesAsync();
                int result = await context.SaveChangesAsync();
                return Ok(result);
           }
           catch (System.Exception ex)
           {
               return BadRequest(ex.Message);
           }
        }

        [HttpPost("updateVehicle")]
        public async Task<ActionResult> updateVehicleAsync([FromBody] VehicleResource vehicleBody){
            try
            {
                Vehicle uptVehicle = await context.Vehicles.AsNoTracking().Include(x=>x.VehicleFeatures).Where(x=>x.Id == vehicleBody.Id).FirstOrDefaultAsync();
                uptVehicle = mapper.Map<VehicleResource,Vehicle>(vehicleBody);
                uptVehicle.VehicleFeatures = new Collection<VehicleFeature>(); 
                foreach(var featureId in vehicleBody.VehicleFeatures){
                    VehicleFeature vf = new VehicleFeature();
                    vf.FeatureId = featureId.FeatureId;
                    vf.VehicleId = uptVehicle.Id;
                    uptVehicle.VehicleFeatures.Add(vf);
                }
                context.Vehicles.Update(uptVehicle);
                int result = await context.SaveChangesAsync();
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("deleteVehicle/{id}")]
        public async Task<IActionResult> deleteVehicleAsync(int id){
            try
            {
                var vehicle = await context.Vehicles.Include(x=>x.VehicleFeatures).Where(x=>x.Id.Equals(id)).FirstOrDefaultAsync();
                context.Vehicles.Remove(vehicle);
                var result = context.SaveChangesAsync();
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getVehicles")]
        public async Task<ActionResult> getVehiclesAsync(){
            var vehiclesResponse = new List<VehicleResource>();
            try
            {
                var vehicles = await context.Vehicles.Include(x=>x.VehicleFeatures).ToListAsync();
                 vehiclesResponse = mapper.Map<List<Vehicle>,List<VehicleResource>>(vehicles);
                return Json(vehiclesResponse);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}