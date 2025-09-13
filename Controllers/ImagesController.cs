using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZwalks.API.Models;
using NZwalks.API.Models.DTO;
using NZwalks.API.Repository.IRepository;

namespace NZwalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImgRepository _imgRepository;

        public ImagesController(IImgRepository imgRepository)
        {
            this._imgRepository = imgRepository;
        }

        [HttpPost]
        [Route("Upload")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Upload([FromForm] UploadImageRequestDto uploadImageRequestDto)
        {
            ValidateFileUplooad(uploadImageRequestDto);
            if (ModelState.IsValid)
            {

                // convert the Dto to the domain model
                var domainImgUPload = new Image
                {
                    File = uploadImageRequestDto.File,
                    FileExtension = Path.GetExtension(uploadImageRequestDto.File.FileName),
                    FileSize = uploadImageRequestDto.File.Length,
                    FileName = uploadImageRequestDto.FileName,
                    FileDescription = uploadImageRequestDto.FileDescription
                };
                // use Repository to upload imge

                await _imgRepository.Upload(domainImgUPload);
                return Ok(domainImgUPload);
            }

            return BadRequest(ModelState);
        }

        private void ValidateFileUplooad(UploadImageRequestDto uploadImageRequestDto)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };
            if (!allowedExtensions.Contains(Path.GetExtension(uploadImageRequestDto.File.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported File Extention");
            }

            if (uploadImageRequestDto.File.Length > 10485760)
            {
                ModelState.AddModelError("File", "The file size is big please enter smaller size");
            }
        }
    }
}
