using api.Dtos.Image;

namespace api.Services.Interfaces;

public interface IUploadImageService
{
  Task<string> UploadImage(ImageDto fileName, string filePathName);
}
