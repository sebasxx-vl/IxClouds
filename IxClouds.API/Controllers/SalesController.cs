using AutoMapper;
using IxClouds.API.DTOs.Request;
using IxClouds.API.DTOs.Response;
using IxClouds.Domain.Entities;
using IxClouds.Domain.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;
namespace IxClouds.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class SalesController : ControllerBase
{
    private readonly ISaleService _saleService;
    private readonly IMapper _mapper;
    public SalesController(ISaleService saleService, IMapper mapper) { _saleService = saleService; _mapper = mapper; }
    [HttpPost] public async Task<ActionResult<SaleResponseDto>> CreateSale([FromBody] CreateSaleRequestDto dto) { if (!ModelState.IsValid) return BadRequest(ModelState); var items = dto.Items.Select(i => (i.ProductId, i.Quantity)).ToList(); try { var sale = await _saleService.RegisterSaleAsync(new Sale(), items); return Ok(_mapper.Map<SaleResponseDto>(sale)); } catch (Exception ex) { return BadRequest(ex.Message); } }
    [HttpGet] public async Task<ActionResult<IEnumerable<SaleResponseDto>>> GetAllSales() { var sales = await _saleService.GetAllSalesAsync(); return Ok(_mapper.Map<IEnumerable<SaleResponseDto>>(sales)); }
    [HttpGet("{id}")] public async Task<ActionResult<SaleResponseDto>> GetSaleById(int id) { var sale = await _saleService.GetSaleByIdAsync(id); if (sale == null) return NotFound(); return Ok(_mapper.Map<SaleResponseDto>(sale)); }
}
