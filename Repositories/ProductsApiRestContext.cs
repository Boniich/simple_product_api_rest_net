using Microsoft.EntityFrameworkCore;
using ProductApiRest.Dtos;

namespace ProductApiRest.Repositories
{
    public class ProductsApiRestContext : DbContext
    {
        public ProductsApiRestContext(DbContextOptions<ProductsApiRestContext> options) : base(options) { }

        public DbSet<ProductEntity> Products { get; set; }

        public async Task<ProductEntity> Get(int id)
        {
            return await Products.FirstAsync(x => x.Id == id);
        }

        public async Task<ProductEntity> Add(CreateProductDto product)
        {
            ProductEntity entity = new ProductEntity()
            {
                Id = null,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
            };

            var response = await Products.AddAsync(entity);
            await SaveChangesAsync();
            return await Get(response.Entity.Id ?? throw new Exception("No se ha podido guardar"));
        }

        public async Task<bool> Update(ProductEntity product)
        {
            Products.Update(product);
            await SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await Get(id);
            Products.Remove(entity);
            SaveChanges();
            return true;
        }
        
    }


    public class ProductEntity
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public ProductDtos ToDto()
        {
            return new ProductDtos()
            {
                Id = Id ?? throw new Exception("El id no puede ser null"),
                Name = Name,
                Description = Description,
                Price = Price
            };
        }
    }
}
