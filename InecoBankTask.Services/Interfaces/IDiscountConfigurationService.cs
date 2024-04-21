using InecoBankTask.Domain.Enums;
using InecoBankTask.Domain.Models;

namespace InecoBankTask.Services.Interfaces
{
    public interface IDiscountConfigurationService
    {
        void SetDiscount(DateTime date, DiscountType type, double value);

        Discount GetDiscount(DateTime date);
    }
}
