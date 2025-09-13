using System.Linq.Expressions;

namespace NZwalks.API.Repository.IRepository;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(Expression<Func<T, bool>> filter);
    Task<T> AddAsync(T entitiy);
    Task<T> RemoveByIdAsync(T entitiy);

}
