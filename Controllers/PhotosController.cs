using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vegas.Core;
using Vegas.Core.Models;

namespace Vegas.Controllers
{
    [Route("/api/vehicle/{vehicleId}/photos")]
    public class PhotosController : Controller
    {
        private readonly IHostingEnvironment host;
        private readonly IVehicleRepository reporitory;
        private readonly IUnitOfWork unitOfWork;
        public PhotosController(IHostingEnvironment host, IVehicleRepository reporitory, IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.reporitory = reporitory;
            this.host = host;
        }
        [HttpPost]
        public async Task<IActionResult> Upload(int vehicleId, IFormFile file)
        {
            var vehicle = await reporitory.GetVehicleAsync(vehicleId, false);
            if(vehicle == null)
                return NotFound();
            var uploadFolderPath = Path.Combine(host.WebRootPath, "uploads");
            if(!Directory.Exists(uploadFolderPath))
                Directory.CreateDirectory(uploadFolderPath);
            
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(uploadFolderPath);
            var filePath = Path.Combine(uploadFolderPath, fileName);

            using (var stream = new FileStream(filePath,FileMode.Create)){
                await stream.CopyToAsync(stream);
            }
            
            var photo = new Photo { FileName = fileName };
            vehicle.Photos.Add(photo);
            return Ok(vehicle);
        }
    }
}