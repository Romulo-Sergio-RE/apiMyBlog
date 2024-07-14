using api.Dtos.Image;
using api.Services.Interfaces;

namespace api.Services
{
    public class UploadImageService : IUploadImageService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UploadImageService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public string UploadImage(ImageDto imageUpdate, string filePathName)
        {
            try
            {
                if (imageUpdate.fileName.Length > 0)
                {
                    string path = _webHostEnvironment.WebRootPath + "\\{filePathName}\\";
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (FileStream fileStream = System.IO.File.Create(path + imageUpdate.fileName.FileName))
                    {
                        imageUpdate.fileName.CopyTo(fileStream);
                        fileStream.Flush();
                        return "upload Done.";
                    }
                }
                else
                {
                    return "Failed.";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}