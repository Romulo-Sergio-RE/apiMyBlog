using api.Dtos.Image;

namespace api.Services.Interfaces;

public interface IUploadImageService
{
    string UploadImage(ImageDto fileName, string filePathName);
}
