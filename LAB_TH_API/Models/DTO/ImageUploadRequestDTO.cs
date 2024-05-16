using System.ComponentModel.DataAnnotations;

namespace LAB_TH_API.Models.DTO
{
    public class ImageUploadRequestDTO
    {
        [Required]
        public IFormFile File { get; set; }
        [Required]
        public string FiliName { get; set; }
        public string? FileDescription { get; set; }
    }
}
