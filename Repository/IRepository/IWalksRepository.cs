using NZwalks.API.Models;

namespace NZwalks.API.Repository.IRepository
{
    public interface IWalksRepository : IRepository<Walk>
    {
        Task<Walk> UpdateAsync(Walk walk);
        Task<IEnumerable<Walk>> GetAllByFilterAsync(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool? isAscending = true, int pageNumber = 1, int pageSize = 100);
    }
}
