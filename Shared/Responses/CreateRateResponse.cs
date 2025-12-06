namespace Shared;

public class CreateRateResponse
{
    public Guid Id { get; set; }

    public decimal RatePercentage { get; set; }
    public string RateName { get; set; }
    public string RateDescription { get; set; }
    public decimal? RateRoundingMargin { get; set; }

    public CreateCurrencyResponse RateCurrency { get; set; }
}
