using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using ORM.Contexts;
using ORM.Models;
using System.Linq.Expressions;

namespace DataAccess;

public class BaseRepository<TEntity>(BaseContext context) : IBaseRepository<TEntity>
    where TEntity : BaseModel
{
    private readonly BaseContext _context = context;

    public virtual void Create(TEntity entity)
    {
        entity.CreatedOn = DateTime.UtcNow;
        entity.UpdatedOn = DateTime.UtcNow;
        _context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
    }

    public virtual void Update(TEntity entity)
    {
        entity.UpdatedOn = DateTime.UtcNow;
        _context.Entry(entity).State = EntityState.Modified;
    }

    public virtual void UpdateRange(IList<TEntity> entities)
    {
        entities.ToList().ForEach(entry => {
            entry.UpdatedOn = DateTime.UtcNow;
            _context.Entry(entry).State = EntityState.Modified;
        });
    }

    protected virtual IQueryable<TEntity> GetQueryable(
        Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        string includeProperties = null,
        int? skip = null,
        int? take = null)
    {
        includeProperties ??= string.Empty;
        IQueryable<TEntity> query = _context.Set<TEntity>();

        if (filter != null)
        {
            query = query.Where(filter);
        }

        foreach (var includeProperty in includeProperties.Split([','], StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        if (orderBy != null)
        {
            query = orderBy(query);
        }

        if (skip.HasValue)
        {
            query = query.Skip(skip.Value);
        }

        if (take.HasValue)
        {
            query = query.Take(take.Value);
        }

        return query;
    }

    public virtual IEnumerable<TEntity> GetAll(
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        string includeProperties = null,
        int? skip = null,
        int? take = null)
    {
        return GetQueryable(null, orderBy, includeProperties, skip, take).ToList();
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        string includeProperties = null,
        int? skip = null,
        int? take = null)
    {
        return await GetQueryable(null, orderBy, includeProperties, skip, take).ToListAsync();
    }

    public virtual IEnumerable<TEntity> Get(
        Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        string includeProperties = null,
        int? skip = null,
        int? take = null)
    {
        return GetQueryable(filter, orderBy, includeProperties, skip, take).ToList();
    }

    public virtual async Task<IEnumerable<TEntity>> GetAsync(
        Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        string includeProperties = null,
        int? skip = null,
        int? take = null)
    {
        return await GetQueryable(filter, orderBy, includeProperties, skip, take).ToListAsync();
    }

    public virtual TEntity? GetFirst(
        Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        string includeProperties = null,
        int? skip = null,
        int? take = null)
    {
        return GetQueryable(filter, orderBy, includeProperties, skip, take).FirstOrDefault();
    }

    public virtual async Task<TEntity?> GetFirstAsync(
        Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        string includeProperties = null,
        int? skip = null,
        int? take = null)
    {
        return await GetQueryable(filter, orderBy, includeProperties, skip, take).FirstOrDefaultAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
