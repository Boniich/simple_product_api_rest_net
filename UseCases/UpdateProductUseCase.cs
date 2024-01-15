using ProductApiRest.Dtos;
using ProductApiRest.Repositories;

namespace ProductApiRest.UseCases
{
    public interface IUpdateProductUseCase
    {
        Task<ProductDtos> Execute(ProductDtos product);
    }

    public class UpdateProductUseCase : IUpdateProductUseCase
    {

        private readonly ProductsApiRestContext _context;

        public UpdateProductUseCase(ProductsApiRestContext context)
        {
            _context = context;
        }

        public async Task<ProductDtos?> Execute(ProductDtos product)
        {
            var entity = await _context.Get(product.Id);

            if(entity == null)
            {
                return null;
            }

            entity.Name = product.Name;
            entity.Description = product.Description;
            entity.Price = product.Price;

            await _context.Update(entity);
            return entity.ToDto();
        }
    }
}
