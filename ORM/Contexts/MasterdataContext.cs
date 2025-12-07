using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ORM.Configurations;
using ORM.Models;
using ORM.Models.MasterData;
namespace ORM.Contexts;

public class MasterdataContext(IConfiguration configuration, DateTime? pointInTime = default) : BaseContext
{
    protected DateTime PointInTime { get; } = pointInTime ?? DateTime.UtcNow;

    protected readonly IConfiguration _configuration = configuration;

    public DbSet<Rate> Rates { get; set; }
    public DbSet<Currency> Currencies { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected void SetQueryFilter<TEntity>(ModelBuilder modelBuilder) where TEntity : TemporalEntity
    {
        modelBuilder.Entity<TEntity>().HasQueryFilter((TEntity e) => EF.Property<DateTime>(e, "ValidFrom") <= PointInTime && EF.Property<DateTime>(e, "ValidTo") >= PointInTime);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        SetQueryFilter<Rate>(modelBuilder);
        SetQueryFilter<Currency>(modelBuilder);
        SetQueryFilter<Category>(modelBuilder);

        modelBuilder.ApplyConfiguration(new RateConfigurations());

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(TemporalEntity).IsAssignableFrom(entityType.ClrType))
            {
                modelBuilder.Entity(entityType.ClrType, builder =>
                {
                    builder.ToTable(entityType.ClrType.Name, b => b.IsTemporal(t =>
                    {
                        t.UseHistoryTable(entityType.ClrType.Name + "History");
                        t.HasPeriodStart("ValidFrom");
                        t.HasPeriodEnd("ValidTo");
                    }));
                });
            }
        }
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        if (!options.IsConfigured)
        {
            var connectionString = _configuration.GetConnectionString("MdDbConnection");
            options.UseSqlServer(connectionString);
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }
    }
}
