namespace ORM.Models.OperationalData;

public class Order : BaseSoftDeleteModel
{
    public string OrderName { get; set; }
    public IEnumerable<Product> Products { get; set; }
}
