using System.ComponentModel.DataAnnotations;

namespace ORM.Models;

public class BaseModel
{
    [Key]
    public Guid EntityId { get; set; } = Guid.NewGuid();
    public DateTime CreatedOn { get; set; }
    public DateTime UpdatedOn { get; set; }
}
