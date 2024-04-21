using InecoBankTask.Domain.Models;

namespace InecoBankTask.Services.Interfaces
{
    public interface IXMLService
    {
        Discount GetXMLDiscountTypeValue(string filePath);
    }
}
