namespace ORM.Models.MasterData;

public class Rate : BiTemporalEntity
{
    public decimal RatePercentage { get; set; }
    public string RateName { get; set; }
    public string RateDescription { get; set; }
    public decimal? RateRoundingMargin { get; set; }


    //Relations
    public Guid CurrencyId { get; set; }
    public Currency RateCurrency { get; set; }
}
