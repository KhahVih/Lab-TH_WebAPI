
using LAB_TH_API.Models;

namespace LAB_TH_API.Repository
{
    public interface IImageRepository
    {
        Image Upload(Image image);
        List<Image> GetAllInfoImage();
        (byte[], string, string) DownloadFile(int Id);
    }
}
