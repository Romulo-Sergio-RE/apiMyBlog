using api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controller;

//[Authorize(Roles = "admin")]
[Route("api/[controller]")]
[ApiController]
public class ImageController : ControllerBase
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IUploadImageService _uploadImage;

    public ImageController(IWebHostEnvironment webHostEnvironment, IUploadImageService uploadImage)
    {
        _webHostEnvironment = webHostEnvironment;
        _uploadImage = uploadImage;
    }

    [HttpGet("{fileName}/{imageName}")]
    public async Task<IActionResult> GetImage([FromRoute] string fileName, string imageName)
    {
        string path = _webHostEnvironment.WebRootPath + $"\\{fileName}\\";
        var filePath =  path + imageName;
        if (System.IO.File.Exists(filePath))
        {
            byte[] b = System.IO.File.ReadAllBytes(filePath);
            return File(b, $"image/{imageName}");
        }
        string messageErro = $"A imagem do {fileName} nao foi encontrada.";
        return NotFound(messageErro);
    }
}
