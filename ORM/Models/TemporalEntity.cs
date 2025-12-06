using System;
namespace ORM.Models;

public class TemporalEntity : BaseModel
{
    public DateTime ValidFrom { get; set; }
    public DateTime ValidTo { get; set; }
}
