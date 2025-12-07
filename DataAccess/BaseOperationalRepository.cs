using DataAccess.Interfaces;
using ORM.Contexts;
using ORM.Models;

namespace DataAccess;

public class BaseOperationalRepository<TEntity>(OperationaldataContext context) : BaseRepository<TEntity>(context), IBaseOperationalRepository<TEntity>
    where TEntity : BaseSoftDeleteModel
{
    private readonly OperationaldataContext _context = context;

    public virtual void Delete(TEntity entity)
    {
        entity.UpdatedOn = DateTime.UtcNow;
        entity.DeletedOn = DateTime.UtcNow;
        _context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
    }
}
