using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Vegas.Controllers.Resources;
using Vegas.Core.Models;
using System;
using Vegas.Core;

namespace Vegas.Controllers
{
    [Route("/api/[controller]/")]
    public class VehicleController : Controller
    {
        private readonly IMapper mapper;
        private readonly IVehicleRepository repository;
        private readonly IUnitOfWork unitOfWork;
        public VehicleController(IMapper mapper, IVehicleRepository repository, IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpGet("getMakes")]
        public async Task<IEnumerable<MakeResource>> GetMakes()
        {
            var makes = await repository.GetMakesAsync();
            return mapper.Map<IEnumerable<Make>, IEnumerable<MakeResource>>(makes);
        }

        [HttpGet("getFeatures")]
        public async Task<IEnumerable<KeyValuePairResource>> getFeaturesAsync()
        {
            var features = await repository.GetFeaturesAsync();
            return mapper.Map<IEnumerable<Feature>, IEnumerable<KeyValuePairResource>>(features);
        }

        [HttpPost("addVehicle")]
        public async Task<IActionResult> addVehicleAsync([FromBody] SaveVehicleResource vehicleBody)
        {
            //throw new Exception();
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var newVehicle = mapper.Map<SaveVehicleResource, Vehicle>(vehicleBody);
                newVehicle.LastUpdate = DateTime.Now;
                repository.Add(newVehicle);
                await unitOfWork.CompleteAsync();

                newVehicle = await repository.GetVehicleAsync(newVehicle.Id);

                var returnVehicle = mapper.Map<Vehicle, VehicleResource>(newVehicle);
                return Ok(returnVehicle);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("updateVehicle/{id}")]
        public async Task<IActionResult> updateVehicleAsync(int id, [FromBody] SaveVehicleResource vehicleBody)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var vehicleObj = await repository.GetVehicleAsync(id);

                if (vehicleObj == null)
                    return NotFound();

                mapper.Map<SaveVehicleResource, Vehicle>(vehicleBody, vehicleObj);
                vehicleObj.LastUpdate = DateTime.Now;

                //context.Update(vehicleObj);
                await unitOfWork.CompleteAsync();
                vehicleObj = await repository.GetVehicleAsync(vehicleObj.Id);

                var result = mapper.Map<Vehicle, VehicleResource>(vehicleObj);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("deleteVehicle/{id}")]
        public async Task<IActionResult> deleteVehicleAsync(int id)
        {
            try
            {
                var vehicle = await repository.GetVehicleAsync(id,includeRelatedObjects: false);
                if (vehicle == null)
                {
                    return NotFound();
                }
                repository.Remove(vehicle);
                var result = unitOfWork.CompleteAsync();
                return Ok(result.Status);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getVehicle/{id}")]
        public async Task<IActionResult> getVehicleAsync(int id)
        {
            try
            {
                var vehicle = await repository.GetVehicleAsync(id);
                if (vehicle == null)
                    return NotFound();

                var response = mapper.Map<Vehicle, VehicleResource>(vehicle);
                return Ok(response);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("getVehicles/{pagination}/{quantity}")]
        public async  Task<IActionResult> getVehicles(int pagination, int quantity){
            try
            {
                var vehsObj = await repository.GetVehiclesAsync(pagination,quantity);
                var vehicles = mapper.Map<IEnumerable<Vehicle>,IEnumerable<VehicleResource>>(vehsObj);
                return Ok(vehicles);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}   