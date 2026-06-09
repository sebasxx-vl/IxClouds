using IxClouds.Domain.DTOs.Request;
using IxClouds.Domain.DTOs.Response;
using IxClouds.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace IxClouds.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly ISaleService _saleService;

        public SalesController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        [HttpGet]
        public async Task<ActionResult<List<SaleResponseDto>>> GetAll([FromQuery] DateTime? fromDate, [FromQuery] DateTime? toDate)
        {
            var sales = await _saleService.GetSalesAsync(fromDate, toDate);
            return Ok(sales);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<SaleResponseDto>> GetById(int id)
        {
            var sale = await _saleService.GetSaleByIdAsync(id);
            if (sale == null) return NotFound(new { message = $"Venta ID {id} no encontrada" });
            return Ok(sale);
        }

        [HttpPost]
        public async Task<ActionResult<SaleResponseDto>> Create([FromBody] CreateSaleRequestDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var sale = await _saleService.CreateSaleAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = sale.Id }, sale);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}