namespace ORM.Models;

public class BaseSoftDeleteModel : BaseModel
{
    public DateTime DeletedOn { get; set; }
}
