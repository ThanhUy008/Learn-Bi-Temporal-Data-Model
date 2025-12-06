using BusinessLogic.Services.Interfaces;
using DataAccess.Interfaces;
using ORM.Models.MasterData;
using Shared;

namespace BusinessLogic.Services
{
    public class RateServices(
        ICurrencyServices currencyServices, 
        IBaseRepository<Rate> rateRepo,
        IUnitOfWork unitOfWork
        ) : IRateServices
    {
        private readonly IBaseRepository<Rate> _rateRepo = rateRepo;

        private readonly ICurrencyServices _currencyServices = currencyServices;

        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<IEnumerable<GetRateResponse>> GetAllAsync()
        {
            return (await _rateRepo.GetAllAsync(
                includeProperties: $"RateCurrency"
                )).ToList().Select(x => new GetRateResponse()
            {
                Id = x.EntityId,
                RateCurrency = new GetCurrencyResponse()
                {
                    Id = x.CurrencyId,
                    IsoCurrencyCode = x.RateCurrency?.IsoCurrencyCode,
                    CurrencyName = x.RateCurrency?.CurrencyName,
                },
                RateDescription = x.RateDescription,
                RateName = x.RateName,
                RatePercentage = x.RatePercentage,
                RateRoundingMargin = x.RateRoundingMargin,
            });
        }


        public async Task<CreateRateResponse> CreateNewRate(CreateRateRequest request)
        {
            using var dbTransaction = _unitOfWork.BeginTransaction();

            try
            {
                ArgumentNullException.ThrowIfNull(request);

                var currency = await _currencyServices.CreateUpdateCurrency(request.RateCurrency);

                var rate = await _rateRepo.GetFirstAsync(x => x.EntityId == request.ExistId);

                if (rate == null)
                {
                    rate = new Rate
                    {
                        RateName = request.RateName,
                        RateDescription = request.RateDescription,
                        RatePercentage = request.RatePercentage,
                        RateRoundingMargin = request.RateRoundingMargin,
                        CurrencyId = currency.Id
                    };

                    _rateRepo.Create(rate);
                }
                else
                {
                    rate.RateName = request.RateName;
                    rate.RateDescription = request.RateDescription;
                    rate.RatePercentage = request.RatePercentage;
                    rate.RateRoundingMargin = request.RateRoundingMargin;
                    rate.CurrencyId = currency.Id;

                    _rateRepo.Update(rate);
                }

                await _rateRepo.SaveChangesAsync();

                await dbTransaction.CommitAsync();

                return new CreateRateResponse()
                {
                    RateName = rate.RateName,
                    RateDescription = rate.RateDescription,
                    RatePercentage = rate.RatePercentage,
                    RateRoundingMargin = rate.RateRoundingMargin,
                    Id = rate.EntityId,
                    RateCurrency = currency
                };
            }
            catch (Exception)
            {
                await dbTransaction.RollbackAsync();
                _unitOfWork.ResetContextState();

                return new CreateRateResponse();
            }
        }
    }
}
