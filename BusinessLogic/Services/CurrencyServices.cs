using BusinessLogic.Services.Interfaces;
using DataAccess.Interfaces;
using ORM.Models.MasterData;
using Shared;

namespace BusinessLogic.Services
{
    public class CurrencyServices(IBiTemporalBaseRepository<Currency> currencyRepo) : ICurrencyServices
    {
        private readonly IBiTemporalBaseRepository<Currency> _currencyRepo = currencyRepo;

        public async Task<CreateCurrencyResponse> CreateUpdateCurrency(CreateCurrencyRequest request)
        {
            if (request?.ExistId == null && request?.IsoCurrencyCode == null)
            {
                throw new ArgumentNullException("Currency null");
            }

            var currency = await _currencyRepo.GetFirstAsync(x => x.EntityId == request.ExistId);

            if(currency == null)
            {
                currency = new Currency
                {
                    CurrencyName = request.CurrencyName,
                    IsoCurrencyCode = request.IsoCurrencyCode
                };

                _currencyRepo.Create(currency);
            }
            else
            {
                currency.CurrencyName = request.CurrencyName;
                currency.IsoCurrencyCode = request.IsoCurrencyCode;

                _currencyRepo.Update(currency);
            }

            await _currencyRepo.SaveChangesAsync();

            return new CreateCurrencyResponse() 
            { 
                Id = currency.EntityId,
                CurrencyName = currency.CurrencyName,
                IsoCurrencyCode = currency.IsoCurrencyCode,
            };
        }
    }
}
