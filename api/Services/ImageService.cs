using api.Dtos.Image;
using api.Services.Interfaces;

namespace api.Services;

public class ImageService : IUploadImageService
{
    private readonly IWebHostEnvironment _webHostEnvironment;


    public ImageService(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }
    public async Task<string> UploadImage(ImageDto imageUpdate, string filePathName)
    {

        if (imageUpdate?.fileName?.Length > 0)
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
    public async Task<string> DeleteImage(string fileName, string imageName)
    {
        string path = _webHostEnvironment.WebRootPath + $"\\{fileName}\\";

        var filePath = path + imageName;

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            return "sucesso.";
        }
        return "erro ao deletar.";
    }
}

