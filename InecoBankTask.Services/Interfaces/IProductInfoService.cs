using InecoBankTask.Domain.Models;

namespace InecoBankTask.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductInfo>> GetProducts();

        Task<ProductInfo> AddProduct(ProductInfo productInfo);

        Task<ProductInfo> UpdateProduct(ProductInfo productInfo);
    }
}
