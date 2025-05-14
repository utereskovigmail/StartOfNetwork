using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;


namespace StartOfNetwork;
public class UploadImage
{
    public string ImageName { get; set; }
}

[Route("api/[controller]")]
[ApiController]
public class GalleriesController : ControllerBase
{
    [HttpPost]
    [Route("upload")]
    public async Task<IActionResult> UploadBase64([FromBody] UploadImage model)
    {
        try
        {
            Console.WriteLine(model.ImageName);
            if (model.ImageName.Contains(','))
            {
                model.ImageName = model.ImageName.Split(',')[1];
            }
            
            byte[] byteArray = Convert.FromBase64String(model.ImageName);

            // Читаємо байти як зображення
            using Image image = Image.Load(byteArray);

            // Масштабуємо зображення до 600x600
            image.Mutate(x => x.Resize(
                new ResizeOptions
                {
                    Size = new Size(600, 600), // Максимальний розмір
                    Mode = ResizeMode.Max // Зберігає пропорції без обрізання
                }
            ));

            string folderName = "images";
            string pathFolder = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            
            string imageName = $"{Guid.NewGuid()}.jpg";
            // Шлях до збереження файлу
            string outputFilePath = Path.Combine(pathFolder, imageName);

            image.Save(outputFilePath);
            //Усе пройшло успішно
            return Ok(new { image = imageName });//код 200 - усе пройшло успішно
        }
        catch (Exception e)
        {
            return BadRequest(new { error = e.Message });
        }
    }
}