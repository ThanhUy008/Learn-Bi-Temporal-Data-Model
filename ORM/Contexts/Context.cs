using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ORM.Models.MasterData;
using ORM.Models.OperationalData;
namespace ORM.Contexts;

public class Context(IConfiguration configuration) : DbContext
{
    protected readonly IConfiguration _configuration = configuration;

    public DbSet<Rate> Rates { get; set; }
    public DbSet<Currency> Currencies { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<OrderProduct> OrderProducts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Context).Assembly);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        if (!options.IsConfigured)
        {
            var connectionString = _configuration.GetConnectionString("DbConnection");
            options.UseSqlServer(connectionString);
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }
    }
}
