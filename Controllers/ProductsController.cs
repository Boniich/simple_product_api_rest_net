using Microsoft.AspNetCore.Mvc;
using ProductApiRest.Dtos;
using ProductApiRest.Repositories;
using ProductApiRest.UseCases;

namespace ProductApiRest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly ProductsApiRestContext _productsApiRestContext;
        private readonly IUpdateProductUseCase _updateProductUseCase;

        public ProductsController(ProductsApiRestContext productsApiRestContext, IUpdateProductUseCase updateProductUseCase)
        {
            _productsApiRestContext = productsApiRestContext;
            _updateProductUseCase = updateProductUseCase;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ProductDtos>))]
        public async Task<IActionResult> GetProducts()
        {
            var result = _productsApiRestContext.Products.Select(c=>c.ToDto()).ToList();
            return new OkObjectResult(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type =typeof(ProductDtos))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProduct(int id)
        {
        
            try
            {
                var product = await _productsApiRestContext.Get(id);
                return new OkObjectResult(product.ToDto());
            }
            catch (Exception ex)
            {
                return new NotFoundObjectResult("Producto no encontrado");
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ProductDtos))]
        public async Task<IActionResult> CreateProduct(CreateProductDto createProduct)
        {
            var product = await _productsApiRestContext.Add(createProduct);
            return new CreatedResult($"http://localhost:7241/api/products/{product.Id}", product.ToDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(ProductDtos productDtos)
        {
            var result = await _updateProductUseCase.Execute(productDtos);
            if (result == null) return new NotFoundResult();
            return new OkObjectResult(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _productsApiRestContext.Delete(id);
            return new OkObjectResult(result);
        }
    }
}
