using NZwalks.API.Models;

namespace NZwalks.API.Repository.IRepository;

public interface IRegionRepository : IRepository<Region>
{
    Task<Region> UpdateAsync(Region region);
}