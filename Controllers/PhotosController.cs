using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Vegas.Controllers.Resources;
using Vegas.Core;
using Vegas.Core.Models;

namespace Vegas.Controllers
{
    [Route("/api/vehicle/{vehicleId}/photos")]
    public class PhotosController : Controller
    {
        private readonly IHostingEnvironment host;
        private readonly IVehicleRepository repository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly PhotoSettings photoSettings;
        private readonly IPhotoRepository photoRepository;
        public PhotosController(
            IHostingEnvironment host, 
            IVehicleRepository repository, 
            IUnitOfWork unitOfWork, 
            IMapper mapper, 
            IOptionsSnapshot<PhotoSettings> options,
            IPhotoRepository photoRepository
            )
        {
            this.photoRepository = photoRepository;
            this.photoSettings = options.Value;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.repository = repository;
            this.host = host;
        }
        [HttpPost]
        public async Task<IActionResult> Upload(int vehicleId, IFormFile file)
        {
            var vehicle = await repository.GetVehicleAsync(vehicleId, false);
            if (vehicle == null)
                return NotFound();

            if (file == null)
                return BadRequest("File cant be null");
            if (file.Length == 0)
                return BadRequest("The file cant be empty");
            if (file.Length > photoSettings.MaxBytes)
                return BadRequest("The size of file has benn excedeed");
            if (!photoSettings.IsValid(file.FileName))
                return BadRequest("Format file not allowed");

            var uploadFolderPath = Path.Combine(host.WebRootPath, "uploads");
            if (!Directory.Exists(uploadFolderPath))
                Directory.CreateDirectory(uploadFolderPath);

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadFolderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var photo = new Photo { FileName = fileName };
            vehicle.Photos.Add(photo);
            await unitOfWork.CompleteAsync();
            return Ok(mapper.Map<Photo, PhotoResource>(photo));
        }
        [HttpGet]
        public async Task<IEnumerable<PhotoResource>> GetPhotos(int vehicleId){
            var photos = await photoRepository.GetPhotos(vehicleId);
            return Mapper.Map<IEnumerable<Photo>,IEnumerable<PhotoResource>>(photos);
        }
    }
}