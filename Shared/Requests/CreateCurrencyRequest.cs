namespace Shared;

public class CreateCurrencyRequest : BaseTemporalCreateRequest
{
    public Guid? ExistId { get; set; }
    public string? CurrencyName { get; set; }
    public string? IsoCurrencyCode { get; set; }
}
