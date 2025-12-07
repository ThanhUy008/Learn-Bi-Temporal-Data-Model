using ORM.Models;

namespace DataAccess.Interfaces
{
    public interface IBiTemporalBaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : BiTemporalEntity
    {
    }
}
