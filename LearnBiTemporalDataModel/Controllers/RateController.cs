using BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace LearnBiTemporalDataModel.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RateController(IRateServices rateServices) : ControllerBase
    {
        private readonly IRateServices _rateServices = rateServices;
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _rateServices.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrUpdate(CreateRateRequest createRateRequest)
        {
            return Ok(await _rateServices.CreateNewRate(createRateRequest));
        }
    }
}
