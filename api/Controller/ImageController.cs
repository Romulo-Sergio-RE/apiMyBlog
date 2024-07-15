using api.Dtos.Image;
using api.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace api.Controller;

[Route("api/[controller]")]
[ApiController]
public class ImageController : ControllerBase
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IUploadImageRepository _uploadImage;

    public ImageController(IWebHostEnvironment webHostEnvironment, IUploadImageRepository uploadImage)
    {
        _webHostEnvironment = webHostEnvironment;
        _uploadImage = uploadImage;
    }

    [HttpPost("{articleId}")]
    public async Task<object> UploadImageArticle([FromForm] ImageDto imageUpdate)
    {
        var upload = await _uploadImage.UploadImage(imageUpdate, "articles");

        return upload;
    }
    [HttpPost("user/{userId}")]
    public async Task<string> UploadImageUser([FromForm] ImageDto imageUpdate)
    {
        var upload =  await _uploadImage.UploadImage(imageUpdate, "users");

        return upload;
    }


    [HttpGet("{fileName}")]
    public async Task<IActionResult> GetImage([FromRoute] string fileName)
    {
        string path = _webHostEnvironment.WebRootPath + "\\uploads\\";
        var filePath = path + fileName + ".jpg";

        if (System.IO.File.Exists(filePath))

        {
            byte[] b = System.IO.File.ReadAllBytes(filePath);
            return File(b, "image/jpg");
        }

        return null;
    }
    [HttpGet("user/{fileName}")]
    public async Task<IActionResult> GetImageUser([FromRoute] string fileName)
    {
        string path = _webHostEnvironment.WebRootPath + "\\users\\";
        var filePath = path + fileName;
        if (System.IO.File.Exists(filePath))

        {
            byte[] b = System.IO.File.ReadAllBytes(filePath);
            return File(b, $"image/{fileName}");
        }

        return null;
    }
}
