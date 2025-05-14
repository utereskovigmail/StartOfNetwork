using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers();

var app = builder.Build();


string folderImage = "images";
string folderPath = Path.Combine(Directory.GetCurrentDirectory(), folderImage);

Directory.CreateDirectory(folderPath);

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(folderPath),
    RequestPath = "/images"
});

app.UseAuthorization();
app.MapControllers();
app.Run();