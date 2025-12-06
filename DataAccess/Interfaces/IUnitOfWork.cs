using Microsoft.EntityFrameworkCore.Storage;

namespace DataAccess.Interfaces
{
    public interface IUnitOfWork
    {
        void Save();

        Task SaveAsync();

        void ResetContextState();

        IDbContextTransaction BeginTransaction();
    }
}
