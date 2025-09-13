using Microsoft.EntityFrameworkCore;
using NZwalks.API.Data;
using NZwalks.API.Repository.IRepository;
using System.Linq.Expressions;

namespace NZwalks.API.Repository;

public class Repository<T> : IRepository<T> where T : class
{

    private readonly ApplicationDbContext _db;
    private readonly DbSet<T> dbSet;
    public Repository(ApplicationDbContext db)
    {
        _db = db;
        this.dbSet = _db.Set<T>();
    }

    public async Task<T> AddAsync(T entitiy)
    {
        await dbSet.AddAsync(entitiy);
        return entitiy;

    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {

        return await dbSet.ToListAsync();
    }

    public async Task<T> GetByIdAsync(Expression<Func<T, bool>> filter)
    {
        return await dbSet.FirstOrDefaultAsync(filter);
    }

    public async Task<T> RemoveByIdAsync(T entitiy)
    {
        dbSet.Remove(entitiy);
        return entitiy;
    }

}