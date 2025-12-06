namespace Shared;

public class CreateRateRequest : BaseTemporalCreateRequest
{
    public Guid? ExistId { get; set; }

    public decimal RatePercentage { get; set; }
    public string RateName { get; set; }
    public string RateDescription { get; set; }
    public decimal? RateRoundingMargin { get; set; }

    public CreateCurrencyRequest RateCurrency { get; set; }
}
