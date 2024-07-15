using api.Dtos.Image;

namespace api.Repository.Interface;

public interface IUploadImageRepository
{
  Task<string> UploadImage(ImageDto fileName, string filePathName);
}
