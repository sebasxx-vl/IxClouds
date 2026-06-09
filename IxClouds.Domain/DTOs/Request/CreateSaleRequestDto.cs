using System.ComponentModel.DataAnnotations;

namespace IxClouds.Domain.DTOs.Request
{
    public class CreateSaleRequestDto
    {
        [Required(ErrorMessage = "Debe incluir al menos un producto")]
        [MinLength(1, ErrorMessage = "La venta debe tener al menos un item")]
        public List<SaleItemRequestDto> Items { get; set; } = new();

        [Range(0, 100, ErrorMessage = "El descuento debe estar entre 0% y 100%")]
        public decimal DiscountPercent { get; set; } = 0;

        [Required(ErrorMessage = "El método de pago es obligatorio")]
        [StringLength(50)]
        public string PaymentMethod { get; set; } = "Cash";

        [StringLength(100, ErrorMessage = "El nombre del cliente no puede exceder 100 caracteres")]
        public string CustomerName { get; set; } = "General";

        [StringLength(500, ErrorMessage = "Las notas no pueden exceder 500 caracteres")]
        public string Notes { get; set; } = string.Empty;
    }

    public class SaleItemRequestDto
    {
        [Required(ErrorMessage = "El producto es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "ID de producto inválido")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "La cantidad es obligatoria")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser al menos 1")]
        public int Quantity { get; set; }
    }
}