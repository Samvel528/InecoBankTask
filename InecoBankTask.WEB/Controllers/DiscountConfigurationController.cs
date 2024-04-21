using InecoBankTask.Domain.Enums;
using InecoBankTask.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InecoBankTask.WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountConfigurationController : ControllerBase
    {
        private readonly IDiscountConfigurationService _discountConfigurationService;

        public DiscountConfigurationController(IDiscountConfigurationService discountConfigurationService)
        {
            _discountConfigurationService = discountConfigurationService;
        }

        [HttpPost("SetupDiscount")]
        public void SetupDiscount(DateTime date, DiscountType type, double value)
        {
            _discountConfigurationService.SetDiscount(date, type, value);
        }
    }
}
