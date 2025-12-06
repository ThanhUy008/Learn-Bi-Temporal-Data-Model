using ORM.Models.MasterData;

namespace ORM.Models.OperationalData;

public class Product : BaseSoftDeleteModel
{
    public string ProductName { get; set; }
    public string ProductDescription { get; set; }

    public decimal Price { get; set; }

    public Guid ProductCategoryId { get; set; }
    public Category Category { get; set; }

    public Guid VATId { get; set; }
    public Rate VAT { get; set; }
}
