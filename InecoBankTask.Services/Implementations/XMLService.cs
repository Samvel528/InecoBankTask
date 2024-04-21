using InecoBankTask.Domain.Enums;
using InecoBankTask.Domain.Models;
using InecoBankTask.Services.Interfaces;
using System.Xml.Linq;

namespace InecoBankTask.Services.Implementations
{
    public class XMLService : IXMLService
    {
        public Discount GetXMLDiscountTypeValue(string filePath)
        {
            // Load XML file
            XDocument doc = XDocument.Load(filePath);

            // Get the Discount element
            XElement discountElement = doc.Root.Element("Discount");
            Discount discount = new Discount();

            if (discountElement != null)
            {
                // Check if Percent element exists
                XElement percentElement = discountElement.Element("Percent");
                if (percentElement != null)
                {
                    decimal percentValue = decimal.Parse(percentElement.Value);
                    discount.Value = percentValue;
                    discount.Type = DiscountType.Percent;
                }

                // Check if Fix element exists
                XElement fixElement = discountElement.Element("Fix");
                if (fixElement != null)
                {
                    decimal fixValue = decimal.Parse(fixElement.Value);
                    discount.Value = fixValue;
                    discount.Type = DiscountType.Fix;
                }
            }

            return discount;
        }
    }
}
