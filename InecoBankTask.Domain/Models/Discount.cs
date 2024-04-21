using InecoBankTask.Domain.Enums;

namespace InecoBankTask.Domain.Models
{
    public class Discount
    {
        public DiscountType Type { get; set; }

        public decimal Value { get; set; }
    }
}
