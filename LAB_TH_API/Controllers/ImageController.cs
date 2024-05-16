using LAB_TH_API.Models;
using LAB_TH_API.Models.DTO;
using LAB_TH_API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LAB_TH_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageRepository _imageRepository;
        public ImageController(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }
        [HttpPost]
        [Route("Upload")]
        public IActionResult UploadImage([FromForm] ImageUploadRequestDTO request)
        {
            ValidateFileUpload(request);
            if (ModelState.IsValid)
            {
                var imageDTO = new Image
                {
                    File = request.File,
                    FileExtension = Path.GetExtension(request.File.Name),
                    FileSizeInBytes = request.File.Length,
                    FileName = request.FiliName,
                    FileDescription = request.FileDescription,
                };

                _imageRepository.Upload(imageDTO);
                return Ok(imageDTO);
            }
            return BadRequest("Error");
        }
        private void ValidateFileUpload(ImageUploadRequestDTO request)
        {
            var allowExtensions = new string[] { ".jpg", ".jpeg", ".png" };
            if(!allowExtensions.Contains(Path.GetExtension(request.File.Name)))
            {
                ModelState.AddModelError("file", "file Extension ");
            }
            if (request.File.Length > 1040000)
            {
                ModelState.AddModelError("file", "file size too big, hay upload file < 10M");
            }
        }
        [HttpGet]
        public IActionResult GetAllInfoImage()
        {
            var allimage = _imageRepository.GetAllInfoImage();
            return Ok(allimage);
        }

        [HttpGet]
        [Route("Download")]
        public IActionResult DownloadImage(int id)
        {
            var download = _imageRepository.DownloadFile(id);
            return File(download.Item1, download.Item2, download.Item3);
        }
    }
}
