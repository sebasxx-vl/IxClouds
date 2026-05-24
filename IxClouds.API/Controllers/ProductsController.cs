using AutoMapper;
using IxClouds.API.DTOs.Request;
using IxClouds.API.DTOs.Response;
using IxClouds.Domain.Entities;
using IxClouds.Domain.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;
namespace IxClouds.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly IMapper _mapper;
    public ProductsController(IProductService productService, IMapper mapper) { _productService = productService; _mapper = mapper; }
    [HttpGet] public async Task<ActionResult<IEnumerable<ProductResponseDto>>> GetAllProducts() { var products = await _productService.GetAllProductsAsync(); return Ok(_mapper.Map<IEnumerable<ProductResponseDto>>(products)); }
    [HttpGet("{id}")] public async Task<ActionResult<ProductResponseDto>> GetProductById(int id) { var product = await _productService.GetProductByIdAsync(id); if (product == null) return NotFound(); return Ok(_mapper.Map<ProductResponseDto>(product)); }
    [HttpPost] public async Task<ActionResult<ProductResponseDto>> CreateProduct([FromBody] CreateProductRequestDto dto) { if (!ModelState.IsValid) return BadRequest(ModelState); var product = _mapper.Map<Product>(dto); var created = await _productService.CreateProductAsync(product); return CreatedAtAction(nameof(GetProductById), new { id = created.Id }, _mapper.Map<ProductResponseDto>(created)); }
    [HttpPut("{id}")] public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductRequestDto dto) { if (id != dto.Id) return BadRequest(); var existing = await _productService.GetProductByIdAsync(id); if (existing == null) return NotFound(); await _productService.UpdateProductAsync(_mapper.Map<Product>(dto)); return NoContent(); }
    [HttpDelete("{id}")] public async Task<IActionResult> DeleteProduct(int id) { var existing = await _productService.GetProductByIdAsync(id); if (existing == null) return NotFound(); await _productService.DeleteProductAsync(id); return NoContent(); }
    [HttpGet("search")] public async Task<ActionResult<IEnumerable<ProductResponseDto>>> SearchProducts([FromQuery] SearchProductRequestDto dto) { var products = await _productService.SearchProductsAsync(dto.Brand, dto.PhoneModel, dto.Material, dto.Gender); return Ok(_mapper.Map<IEnumerable<ProductResponseDto>>(products)); }
    [HttpGet("low-stock")] public async Task<ActionResult<IEnumerable<ProductResponseDto>>> GetLowStockProducts() { var products = await _productService.GetLowStockProductsAsync(); return Ok(_mapper.Map<IEnumerable<ProductResponseDto>>(products)); }
}
