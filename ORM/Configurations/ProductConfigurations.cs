using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ORM.Models.OperationalData;

namespace ORM.Configurations;

public class ProductConfigurations : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        //builder
        //    .HasOne(x => x.VAT)
        //    .WithMany()
        //    .HasForeignKey(x => x.VATId);

        //builder
        //    .HasOne(x => x.Category)
        //    .WithMany()
        //    .HasForeignKey(x => x.ProductCategoryId);
    }
}
