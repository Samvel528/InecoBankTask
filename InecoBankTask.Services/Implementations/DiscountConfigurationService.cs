using InecoBankTask.Domain.Enums;
using InecoBankTask.Domain.Models;
using InecoBankTask.Services.Interfaces;
using System.Xml.Linq;

namespace InecoBankTask.Services.Implementations
{
    public class DiscountConfigurationService : IDiscountConfigurationService
    {
        private readonly IXMLService _xmlService;
        private readonly static string dateFormat = "ddMMyyyy";

        public DiscountConfigurationService(IXMLService xmlService)
        {
            _xmlService = xmlService;
        }

        public Discount GetDiscount(DateTime date)
        {
            string fileName = GetFileName(date);

            // Get the project directory
            string projectDirectory = Directory.GetCurrentDirectory();
            string filePath = $"{projectDirectory}/Discount/{fileName}";
            Discount discount = new Discount();

            // Check if the file exists
            if (File.Exists(filePath))
            {
                discount = _xmlService.GetXMLDiscountTypeValue(filePath);
            }

            return discount;
        }

        public void SetDiscount(DateTime date, DiscountType type, double value)
        {
            // Create XML elements
            XElement productInfoElement = new XElement
            ("ProductInfo", new XElement
                ("Discount", new XElement
                    ($"{type}", value)
                )
            );

            // Create XML document with the root element
            XDocument doc = new XDocument(productInfoElement);

            string fileName = GetFileName(date);

            // Get the project directory
            string projectDirectory = Directory.GetCurrentDirectory();

            // Save the XML document to a file in the project directory
            string filePath = Path.Combine($"{projectDirectory}/Discount/{fileName}");
            doc.Save(filePath);
        }

        private string GetFileName(DateTime date)
        {
            string currentDate = date.ToString(dateFormat);
            string fileName = $"{currentDate}.xml";

            return fileName;
        }
    }
}
