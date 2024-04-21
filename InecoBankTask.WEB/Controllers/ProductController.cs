using InecoBankTask.Domain.Models;
using InecoBankTask.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InecoBankTask.WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productInfoService;

        public ProductController(IProductService productInfoService)
        {
            _productInfoService = productInfoService;
        }

        [HttpGet("GetProductInfo")]
        public async Task<IActionResult> GetProductInfo()
        {
            return Ok(await _productInfoService.GetProducts());
        }

        [HttpPost("AddProductInfo")]
        public async Task<IActionResult> AddProductInfo(ProductInfo productInfo)
        {
            return Ok(await _productInfoService.AddProduct(productInfo));
        }

        [HttpPut("UpdateProductInfo")]
        public async Task<IActionResult> UpdateProductInfo(ProductInfo productInfo)
        {
            return Ok(await _productInfoService.UpdateProduct(productInfo));
        }
    }
}
