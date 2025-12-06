namespace ORM.Models;

public class BiTemporalEntity : TemporalEntity
{
    public DateTime RegistrationFrom { get; set; }
    public DateTime RegistrationTo { get; set; }
}
