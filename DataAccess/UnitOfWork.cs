using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ORM.Contexts;

namespace DataAccess
{
    public class UnitOfWork(OperationaldataContext context) : IUnitOfWork
    {
        private readonly OperationaldataContext _context = context;

        public void Save()
        {
            _context.SaveChanges();
            ResetContextState();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
            ResetContextState();
        }

        public void ResetContextState()
        {
            _context.ChangeTracker.Entries()
                .Where(e => e.Entity != null).ToList()
                .ForEach(e => e.State = EntityState.Detached);
        }

        public IDbContextTransaction BeginTransaction()
        {
            return _context.Database.BeginTransaction();
        }
    }
}
