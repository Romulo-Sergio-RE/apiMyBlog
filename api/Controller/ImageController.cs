using api.Dtos.Image;
using api.Repository.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace api.Controller;

[Route("api/[controller]")]
[ApiController]
public class ImageController : ControllerBase
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ImageController(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    [HttpPost]
    public async Task<string> UploadImage([FromForm] ImageDto imageUpdate)
    {
        try
        {
            if (imageUpdate.formFileName.Length > 0)
            {
                string path = _webHostEnvironment.WebRootPath + "\\articles\\";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                using (FileStream fileStream = System.IO.File.Create(path + imageUpdate.formFileName.FileName))
                {
                    imageUpdate.formFileName.CopyTo(fileStream);
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
    [HttpPost("user")]
    public async Task<string> UploadImageUser([FromForm] ImageDto imageUpdate)
    {
        try
        {
            if (imageUpdate.formFileName.Length > 0)
            {
                string path = _webHostEnvironment.WebRootPath + "\\users\\";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                using (FileStream fileStream = System.IO.File.Create(path + imageUpdate.formFileName.FileName))
                {
                    imageUpdate.formFileName.CopyTo(fileStream);
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
    [HttpGet("{fileName}")]
    public async Task<IActionResult> GetImage([FromRoute] string fileName)
    {
        string path = _webHostEnvironment.WebRootPath + "\\uploads\\";
        var filePath = path + fileName + ".png";

        if (System.IO.File.Exists(filePath))

        {
            byte[] b = System.IO.File.ReadAllBytes(filePath);
            return File(b, "image/png");
        }

        return null;
    }
     [HttpGet("user/{fileName}")]
    public async Task<IActionResult> GetImageUser([FromRoute] string fileName)
    {
        string path = _webHostEnvironment.WebRootPath + "\\users\\";
        var filePath = path + fileName + ".png";

        if (System.IO.File.Exists(filePath))

        {
            byte[] b = System.IO.File.ReadAllBytes(filePath);
            return File(b, "image/png");
        }

        return null;
    }
}
