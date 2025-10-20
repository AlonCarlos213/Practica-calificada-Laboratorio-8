using Lab_8___Carlos_Mamani.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab_8___Carlos_Mamani.Repositories.Implementations;

public abstract class RepositoryBase<TEntity> where TEntity : class
{
    protected readonly LINQExampleContext _db;
    protected readonly DbSet<TEntity> _set;

    protected RepositoryBase(LINQExampleContext db)
    {
        _db = db;
        _set = _db.Set<TEntity>();
    }

    public virtual IQueryable<TEntity> Query() => _set.AsQueryable();

    public virtual Task AddAsync(TEntity entity, CancellationToken ct = default) =>
        _set.AddAsync(entity, ct).AsTask();

    public virtual void Remove(TEntity entity) => _set.Remove(entity);
}