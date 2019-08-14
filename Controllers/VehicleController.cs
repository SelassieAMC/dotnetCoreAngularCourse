using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vegas.Controllers.Resources;
using Vegas.Models;
using Vegas.Persistence;
using System;

namespace Vegas.Controllers
{
    [Route("/api/[controller]/")]
    public class VehicleController : Controller
    {
        private readonly VegaDbContext context;
        private readonly VehicleRepository repoVehicle;
        private readonly IMapper mapper;
        public VehicleController(VegaDbContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;
            this.repoVehicle = new VehicleRepository(context);
        }
        [HttpGet("getMakes")]
        public async Task<IEnumerable<MakeResource>> GetMakes(){
            var makes = await context.Makes.Include(m=>m.Models).ToListAsync();
            return mapper.Map<List<Make>,List<MakeResource>>(makes);
        }
        
        [HttpGet("getFeatures")]
        public async Task<IEnumerable<KeyValuePairResource>> getFeaturesAsync(){
            var features = await context.Features.ToListAsync();
            return mapper.Map<IEnumerable<Feature>,IEnumerable<KeyValuePairResource>>(features);
        }
        
        [HttpPost("addVehicle")]
        public async Task<IActionResult> addVehicleAsync([FromBody] SaveVehicleResource vehicleBody){
           try
           {
                if(!ModelState.IsValid)
                    return BadRequest(ModelState);

                var newVehicle = mapper.Map<SaveVehicleResource,Vehicle>(vehicleBody);
                var data =  context.Vehicles.Add(newVehicle);
                await context.SaveChangesAsync();

                newVehicle = await repoVehicle.GetVehicleAsync(newVehicle.Id);

                var returnVehicle = mapper.Map<Vehicle, VehicleResource>(newVehicle);
                return Ok(returnVehicle);
           }
           catch (System.Exception ex)
           {
               return BadRequest(ex.Message);
           }
        }

        [HttpPost("updateVehicle/{id}")]
        public async Task<IActionResult> updateVehicleAsync(int id, [FromBody] SaveVehicleResource vehicleBody){
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var vehicleObj = await repoVehicle.GetVehicleAsync(id);

                if(vehicleObj == null)
                    return NotFound();

                mapper.Map<SaveVehicleResource,Vehicle>(vehicleBody,vehicleObj);
                vehicleObj.LastUpdate = DateTime.Now;

                //context.Update(vehicleObj);
                await context.SaveChangesAsync();
                vehicleObj = await repoVehicle.GetVehicleAsync(vehicleObj.Id);

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
                var vehicle = await context.Vehicles.Include(x=>x.VehicleFeatures).FirstOrDefaultAsync(x=>x.Id.Equals(id));//.Where(x=>x.Id.Equals(id)).FirstOrDefaultAsync();
                if(vehicle == null){
                    return NotFound();
                }
                context.Vehicles.Remove(vehicle);
                var result = context.SaveChangesAsync();
                return Ok(result.Result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getVehicles")]
        public async Task<ActionResult> getVehiclesAsync(){
            var vehiclesResponse = new List<SaveVehicleResource>();
            try
            {
                var vehicles = await context.Vehicles.Include(x=>x.VehicleFeatures).ToListAsync();
                vehiclesResponse = mapper.Map<List<Vehicle>,List<SaveVehicleResource>>(vehicles);
                return Json(vehiclesResponse);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getVehicle/{id}")]
        public async Task<ActionResult> getVehicleAsync(int id){
            try
            {
                var vehicle = await repoVehicle.GetVehicleAsync(id);
                if(vehicle == null)
                    return NotFound();

                var response = mapper.Map<Vehicle,VehicleResource>(vehicle);
                return Ok(response);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}