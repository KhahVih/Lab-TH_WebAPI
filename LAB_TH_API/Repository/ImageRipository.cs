using LAB_TH_API.Data;
using LAB_TH_API.Models;

namespace LAB_TH_API.Repository
{
    public class ImageRipository : IImageRepository
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly AppDbContext _dbContext;
        public ImageRipository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor contextAccessor, AppDbContext dbContext)
        {
            _webHostEnvironment = webHostEnvironment;
            _contextAccessor = contextAccessor;
            _dbContext = dbContext;
        }
        public (byte[], string, string) DownloadFile(int Id)
        {
            try
            {
                var filebyid = _dbContext.Images.Where(x => x.Id == Id).FirstOrDefault();
                var path = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", $"{filebyid.FileName}{filebyid.FileExtension}");
                var stream = File.ReadAllBytes(path);
                var filename = filebyid.FileName + filebyid.FileExtension;
                return (stream, "application/octet-stream", filename);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public Image Upload(Image image)
        {
            var filepath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images",
              $"{image.FileName}{image.FileExtension}");
            using var stream = new FileStream(filepath, FileMode.Create);
            image.File.CopyTo(stream);

            var urlFilepath = $"{_contextAccessor.HttpContext.Request.Scheme}" +
                $"{_contextAccessor.HttpContext.Request.Host}{_contextAccessor.HttpContext.Request.PathBase}" +
                $"{image.FileName}{image.FileExtension}";
            image.FilePath = urlFilepath;
            // add image

            _dbContext.Images.Add(image);
            _dbContext.SaveChanges();

            return image;
        }

        public List<Image> GetAllInfoImage()
        {

            var allimages = _dbContext.Images.ToList();
            return allimages;
        }
    }
}
