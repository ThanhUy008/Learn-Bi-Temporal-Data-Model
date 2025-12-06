namespace Shared;

public class GetRateResponse
{
    public Guid Id { get; set; }

    public decimal RatePercentage { get; set; }
    public string RateName { get; set; }
    public string RateDescription { get; set; }
    public decimal? RateRoundingMargin { get; set; }

    public GetCurrencyResponse RateCurrency { get; set; }
}
