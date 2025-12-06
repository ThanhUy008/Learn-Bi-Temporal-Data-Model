using Shared;

namespace BusinessLogic.Services.Interfaces
{
    public interface ICurrencyServices
    {
        public Task<CreateCurrencyResponse> CreateUpdateCurrency(CreateCurrencyRequest request);
    }
}
