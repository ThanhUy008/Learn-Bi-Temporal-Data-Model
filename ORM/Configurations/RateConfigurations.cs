using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ORM.Models.MasterData;

namespace ORM.Configurations;

public class RateConfigurations : IEntityTypeConfiguration<Rate>
{
    public void Configure(EntityTypeBuilder<Rate> builder)
    {
        builder
            .HasOne(r => r.RateCurrency)
            .WithMany()
            .HasForeignKey(x => x.CurrencyId);
    }
}
