using InecoBankTask.Domain.Enums;
using InecoBankTask.Domain.Models;
using InecoBankTask.Services.Interfaces;

namespace InecoBankTask.Services.Implementations
{
    internal class DiscountService : IDiscountService
    {
        private readonly IDiscountConfigurationService _discountConfigurationService;
        private readonly IProductService _productInfoService;

        public DiscountService(IDiscountConfigurationService discountConfigurationService, IProductService productInfoService)
        {
            _discountConfigurationService = discountConfigurationService;
            _productInfoService = productInfoService;
        }

        public async Task SetDiscount()
        {
            var today = DateTime.Today;
            Discount discount = _discountConfigurationService.GetDiscount(today);
            var productInfos = await GetProducts();

            if (discount.Value != 0)
            {
                Func<decimal, decimal, decimal?> priceCalc;

                switch (discount.Type)
                {
                    case DiscountType.Percent:
                        priceCalc = (price, disc) => price - price * disc / 100;
                        break;
                    case DiscountType.Fix:
                        priceCalc = (price, disc) => Math.Max(price - disc, 0);
                        break;
                    default:
                        throw new NotImplementedException($"DiscountType.{discount.Type}");
                }

                foreach (var productInfo in productInfos)
                {
                    if (productInfo.Price.HasValue)
                    {
                        productInfo.Price = priceCalc(productInfo.Price.Value, discount.Value);
                        await _productInfoService.UpdateProduct(productInfo);
                    }
                }
            }
        }

        private async Task<List<ProductInfo>> GetProducts()
        {
            return await _productInfoService.GetProducts();
        }
    }
}
