using NZwalks.API.Models;

namespace NZwalks.API.Repository.IRepository
{
    public interface IImgRepository
    {
        Task<Image> Upload(Image image);
    }
}
