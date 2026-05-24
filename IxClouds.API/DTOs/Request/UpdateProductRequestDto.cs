using System.ComponentModel.DataAnnotations;
namespace IxClouds.API.DTOs.Request;
public class UpdateProductRequestDto
{
    public int Id { get; set; }
    [Required][MaxLength(100)] public string Brand { get; set; } = string.Empty;
    [Required][MaxLength(100)] public string PhoneModel { get; set; } = string.Empty;
    [MaxLength(20)] public string Gender { get; set; } = string.Empty;
    [MaxLength(50)] public string Material { get; set; } = string.Empty;
    [Range(0, int.MaxValue)] public int Stock { get; set; }
    [Range(0.01, double.MaxValue)] public decimal PurchasePrice { get; set; }
    [Range(0.01, double.MaxValue)] public decimal SalePrice { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
}
