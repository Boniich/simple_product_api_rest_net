using System.ComponentModel.DataAnnotations;

namespace ProductApiRest.Dtos
{
    public class CreateProductDto
    {

        [Required(ErrorMessage ="El nombre del producto es requerido")]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "El precio del producto es requerido")]
        public decimal Price { get; set; }
    }
}
