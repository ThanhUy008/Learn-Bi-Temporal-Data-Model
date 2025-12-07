using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ORM.Configurations;
using ORM.Models.OperationalData;
namespace ORM.Contexts;

public class OperationaldataContext(IConfiguration configuration) : BaseContext
{
    protected readonly IConfiguration _configuration = configuration;

    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<OrderProduct> OrderProducts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //modelBuilder.ApplyConfigurationsFromAssembly(typeof(OperationaldataContext).Assembly);
        modelBuilder.ApplyConfiguration(new OrderProductConfigurations());
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
