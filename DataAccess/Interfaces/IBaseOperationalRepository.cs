using ORM.Models;

namespace DataAccess.Interfaces
{
    public interface IBaseOperationalRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : BaseSoftDeleteModel
    {
        public void Delete(TEntity entity);
    }
}
