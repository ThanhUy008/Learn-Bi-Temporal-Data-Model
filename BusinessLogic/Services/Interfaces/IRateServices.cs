using Shared;

namespace BusinessLogic.Services.Interfaces
{
    public interface IRateServices
    {
        public Task<IEnumerable<GetRateResponse>> GetAllAsync();

        public Task<CreateRateResponse> CreateNewRate(CreateRateRequest request);
    }
}
