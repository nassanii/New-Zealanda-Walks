using NZwalks.API.Data;
using NZwalks.API.Models;
using NZwalks.API.Repository.IRepository;

namespace NZwalks.API.Repository;

public class RegionRepository : Repository<Region>, IRegionRepository
{

    private readonly ApplicationDbContext _db;
    public RegionRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }
    public async Task<Region> UpdateAsync(Region region)
    {

        _db.regions.Update(region);
        await _db.SaveChangesAsync();

        return region;
    }
}
