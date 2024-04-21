using InecoBankTask.Domain.Models;
using InecoBankTask.Entities.Context;
using InecoBankTask.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InecoBankTask.Services.Implementations
{
    public class ProductInfoService : IProductService
    {
        private readonly InecoBankTaskContext _context;

        public ProductInfoService(InecoBankTaskContext context)
        {
            _context = context;
        }

        public async Task<ProductInfo> AddProduct(ProductInfo productInfo)
        {
            var model = new Entities.Models.ProductInfo
            {
                ProductName = productInfo.ProductName,
                Description = productInfo.Description,
                Price = productInfo.Price,
            };

            var entity = await _context.ProductInfos.AddAsync(model);
            await _context.SaveChangesAsync();

            return new ProductInfo()
            {
                ProductId = entity.Entity.ProductId,
                Description = entity.Entity.Description,
                Price = entity.Entity.Price,
                ProductName = entity.Entity.ProductName,
            };
        }

        public async Task<List<ProductInfo>> GetProducts()
        {
            var productInfos = await _context.ProductInfos.ToListAsync();
            List<ProductInfo> result = new List<ProductInfo>();

            foreach (var productInfo in productInfos)
            {
                result.Add(new ProductInfo()
                {
                    ProductId = productInfo.ProductId,
                    ProductName = productInfo.ProductName,
                    Description = productInfo.Description,
                    Price = productInfo.Price,
                });
            }

            return result;
        }

        public async Task<ProductInfo> UpdateProduct(ProductInfo productInfo)
        {
            var entity = await _context.ProductInfos.FirstOrDefaultAsync(p => p.ProductId == productInfo.ProductId)
                ?? throw new Exception($"Product not found: Id = {productInfo.ProductId}");

            entity.ProductName = productInfo.ProductName;
            entity.Description = productInfo.Description;
            entity.Price = productInfo.Price;

            _context.ProductInfos.Update(entity);
            await _context.SaveChangesAsync();

            return new ProductInfo()
            {
                ProductId = productInfo.ProductId,
                ProductName = productInfo.ProductName,
                Description = productInfo.Description,
                Price = productInfo.Price,
            };
        }
    }
}
