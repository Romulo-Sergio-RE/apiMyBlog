using api.Dtos.Image;
using api.Interface;
using api.Services.Interfaces;

namespace api.Services;

public class ImageService : IUploadImageService
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IUserRepository _userRepo;

    public ImageService(IWebHostEnvironment webHostEnvironment, IUserRepository userRepo)
    {
        _webHostEnvironment = webHostEnvironment;
        _userRepo = userRepo;
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

        string messageErro;
        string path = _webHostEnvironment.WebRootPath + $"\\{fileName}\\";
        var filePath = path + imageName;
        if (System.IO.File.Exists(filePath))
        {          
            System.IO.File.Delete(filePath);  
            messageErro = $"A imagem do {fileName} foi deletado";
            return messageErro;
        }
        messageErro = $"A imagem do {fileName} nao foi encontrada.";
        return messageErro;

    }
}

