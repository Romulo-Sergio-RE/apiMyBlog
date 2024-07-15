using api.Dtos.Image;
using api.Interface;
using api.Repository.Interface;

namespace api.Repository;

public class UploadImageRepository : IUploadImageRepository
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IUserRepository _userRepo;

    public UploadImageRepository(IWebHostEnvironment webHostEnvironment, IUserRepository userRepo)
    {
        _webHostEnvironment = webHostEnvironment;
        _userRepo = userRepo;
    }
    public async Task<string> UploadImage(ImageDto imageUpdate, string filePathName)
    {

        if (imageUpdate.fileName.Length > 0)
        {
            string path = _webHostEnvironment.WebRootPath + $"\\{filePathName}\\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            using (FileStream fileStream = File.Create(path + imageUpdate.fileName.FileName))
            {
                imageUpdate.fileName.CopyTo(fileStream);
                fileStream.Flush();
                return imageUpdate.fileName.FileName;
            }
        }
        else
        {
            return "Failed.";
        }

    }
}