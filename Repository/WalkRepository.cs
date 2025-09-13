using Microsoft.EntityFrameworkCore;
using NZwalks.API.Data;
using NZwalks.API.Models;
using NZwalks.API.Repository.IRepository;

namespace NZwalks.API.Repository
{
    public class WalkRepository : Repository<Walk>, IWalksRepository
    {
        private readonly ApplicationDbContext _db;

        public WalkRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Walk>> GetAllByFilterAsync(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool? isAscending = true, int pageNumber = 1, int pageSize = 100)
        {
            var walks = _db.walks.Include("Region").Include("Difficulty").AsQueryable();

            // Filtering
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }
            }

            // sorting 
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = (isAscending ?? true)
                                       ? walks.OrderBy(x => x.Name)
                                           : walks.OrderByDescending(x => x.Name);
                }
                else if (sortBy.Equals("LengthInKm", StringComparison.OrdinalIgnoreCase))
                {
                    walks = (isAscending ?? true)
                                       ? walks.OrderBy(x => x.LengthInKm)
                                           : walks.OrderByDescending(x => x.LengthInKm);
                }
            }

            // pagination
            var SkipResult = (pageNumber - 1) * pageSize;
            return await walks.Skip(SkipResult).Take(pageSize).ToListAsync();

            //return await dbSet.Include("Region").Include("Difficulty").ToListAsync();

        }

        public async Task<Walk> UpdateAsync(Walk walk)
        {
            _db.walks.Update(walk);
            await _db.SaveChangesAsync();
            return walk;
        }
    }
}
