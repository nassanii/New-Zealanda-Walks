using NZwalks.API.Data;
using NZwalks.API.Models;
using NZwalks.API.Repository.IRepository;

namespace NZwalks.API.Repository
{
    public class ImageRepository : IImgRepository
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _db;

        public ImageRepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, ApplicationDbContext db)
        {
            this._webHostEnvironment = webHostEnvironment;
            this._httpContextAccessor = httpContextAccessor;
            this._db = db;
        }
        public async Task<Image> Upload(Image image)
        {
            var LocalfilePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", $"{image.FileName}{image.FileExtension}");

            // upload img to the local path
            using var stream = new FileStream(LocalfilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            // https://localhost:1234/images/image.jpg {request.Scheme}://{request.Host}/Images/{image.FileName}{image.FileExtension}
            var urlFilePath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}/Images/{image.FileName}{image.FileExtension}";

            image.FilePath = urlFilePath;

            // save to the db
            await _db.images.AddAsync(image);
            await _db.SaveChangesAsync();
            return image;


        }
    }
}
