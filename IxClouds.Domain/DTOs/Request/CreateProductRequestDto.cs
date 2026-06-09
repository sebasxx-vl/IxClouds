using System.ComponentModel.DataAnnotations;

namespace IxClouds.Domain.DTOs.Request
{
    public class CreateProductRequestDto
    {
        [Required(ErrorMessage = "El nombre del producto es obligatorio")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 100 caracteres")]
        public string Name { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "La descripción no puede exceder 500 caracteres")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "El SKU es obligatorio")]
        [StringLength(50, ErrorMessage = "El SKU no puede exceder 50 caracteres")]
        public string Sku { get; set; } = string.Empty;

        [Required(ErrorMessage = "El precio es obligatorio")]
        [Range(0.01, 999999.99, ErrorMessage = "El precio debe ser mayor a 0")]
        public decimal Price { get; set; }

        [Range(0, 999999.99, ErrorMessage = "El costo no puede ser negativo")]
        public decimal Cost { get; set; }

        [Required(ErrorMessage = "La cantidad en stock es obligatoria")]
        [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo")]
        public int StockQuantity { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "El stock mínimo no puede ser negativo")]
        public int MinStockLevel { get; set; } = 10;

        [Required(ErrorMessage = "La categoría es obligatoria")]
        [StringLength(50, ErrorMessage = "La categoría no puede exceder 50 caracteres")]
        public string Category { get; set; } = string.Empty;

        [Url(ErrorMessage = "La URL de imagen no es válida")]
        [StringLength(500, ErrorMessage = "La URL no puede exceder 500 caracteres")]
        public string ImageUrl { get; set; } = string.Empty;
    }
}