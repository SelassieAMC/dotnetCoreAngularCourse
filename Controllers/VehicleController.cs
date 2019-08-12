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
using System;

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
        public async Task<IActionResult> addVehicleAsync([FromBody] VehicleResource vehicleBody){
           try
           {
                if(!ModelState.IsValid)
                    return BadRequest(ModelState);

                var newVehicle = mapper.Map<VehicleResource,Vehicle>(vehicleBody);
                var data =  context.Vehicles.Add(newVehicle);
                //await context.SaveChangesAsync();
                await context.SaveChangesAsync();
                var returnVehicle = mapper.Map<Vehicle, VehicleResource>(newVehicle);
                return Ok(returnVehicle);
           }
           catch (System.Exception ex)
           {
               return BadRequest(ex.Message);
           }
        }

        [HttpPost("updateVehicle/{id}")]
        public async Task<IActionResult> updateVehicleAsync(int id, [FromBody] VehicleResource vehicleBody){
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                //var uptVehicle = await context.Vehicles.AsNoTracking().Include(x=>x.VehicleFeatures).Where(x=>x.Id == id).FirstOrDefaultAsync();
                var vehicleObj = await context.Vehicles.Include(x=>x.VehicleFeatures).FirstOrDefaultAsync(y=>y.Id.Equals(id));
                mapper.Map<VehicleResource,Vehicle>(vehicleBody,vehicleObj);
                vehicleObj.LastUpdate = DateTime.Now;
                await context.SaveChangesAsync();
                var result = mapper.Map<Vehicle,VehicleResource>(vehicleObj);
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