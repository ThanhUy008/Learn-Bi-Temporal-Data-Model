using DataAccess.Interfaces;
using ORM.Contexts;
using ORM.Models;

namespace DataAccess;

public class BiTemporalBaseRepository<TEntity>(MasterdataContext context) : BaseRepository<TEntity>(context), IBiTemporalBaseRepository<TEntity>
    where TEntity : BiTemporalEntity
{
    private readonly MasterdataContext _context = context;
}
