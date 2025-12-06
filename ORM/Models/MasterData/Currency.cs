using System;
namespace ORM.Models.MasterData;

public class Currency : BiTemporalEntity
{
    public string CurrencyName { get; set; }
    public string IsoCurrencyCode { get; set; }
}
