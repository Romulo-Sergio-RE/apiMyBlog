using api.Dtos.Image;
using api.Repository.Interface;
using api.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace api.Controller;

[Route("api/[controller]")]
[ApiController]
public class ImageController : ControllerBase
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly UploadImageService _uploadImage;

    public ImageController(IWebHostEnvironment webHostEnvironment, UploadImageService uploadImage)
    {
        _webHostEnvironment = webHostEnvironment;
        _uploadImage = uploadImage;
    }

    [HttpPost]
    public string UploadImageArticle([FromForm] ImageDto imageUpdate)
    {
       var upload =  _uploadImage.UploadImage(imageUpdate, "articles");

       return upload;
    }
    [HttpPost("user")]
    public string UploadImageUser([FromForm] ImageDto imageUpdate)
    {
        var upload =  _uploadImage.UploadImage(imageUpdate, "users");

       return upload;
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
