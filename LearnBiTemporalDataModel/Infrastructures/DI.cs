using BusinessLogic.Services;
using BusinessLogic.Services.Interfaces;
using DataAccess;
using DataAccess.Interfaces;
using ORM.Contexts;

namespace LearnBiTemporalDataModel.Infrastructures;

public static class DI
{
    public static void RegisterDatabaseConfigurations(this IServiceCollection services)
    {
        services.AddDbContext<Context>();

        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddTransient<ICurrencyServices, CurrencyServices>();
        services.AddTransient<IRateServices, RateServices>();
    }
}
