using IxCloud.DataAccess;
using IxCloud.DataAccess.Context;
using IxClouds.Domain.DTOs.Request;
using IxClouds.Domain.DTOs.Response;
using IxClouds.Domain.Entities;
using IxClouds.Domain.Interfaces.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace IxClouds.Domain.Services
{
    public class SaleService : ISaleService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<SaleService> _logger;

        public SaleService(ApplicationDbContext context, ILogger<SaleService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<SaleResponseDto> CreateSaleAsync(CreateSaleRequestDto dto)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                foreach (var item in dto.Items)
                {
                    var product = await _context.Products.FindAsync(item.ProductId);
                    if (product == null)
                        throw new InvalidOperationException($"Producto ID {item.ProductId} no encontrado");
                    if (product.StockQuantity < item.Quantity)
                        throw new InvalidOperationException(
                            $"Stock insuficiente para '{product.Name}'. Disponible: {product.StockQuantity}, Solicitado: {item.Quantity}");
                    if (!product.IsActive)
                        throw new InvalidOperationException($"El producto '{product.Name}' está inactivo");
                }

                var sale = new Sale
                {
                    InvoiceNumber = await GenerateInvoiceNumberAsync(),
                    SaleDate = DateTime.UtcNow,
                    Discount = dto.DiscountPercent,
                    PaymentMethod = dto.PaymentMethod,
                    CustomerName = dto.CustomerName ?? "General",
                    Notes = dto.Notes
                };

                decimal totalAmount = 0;

                foreach (var itemDto in dto.Items)
                {
                    var product = await _context.Products.FindAsync(itemDto.ProductId);

                    product!.StockQuantity -= itemDto.Quantity;
                    product.UpdatedAt = DateTime.UtcNow;

                    var saleItem = new SaleItem
                    {
                        ProductId = itemDto.ProductId,
                        Quantity = itemDto.Quantity,
                        UnitPrice = product.Price,
                        TotalPrice = product.Price * itemDto.Quantity
                    };

                    totalAmount += saleItem.TotalPrice;
                    sale.Items.Add(saleItem);
                }

                sale.TotalAmount = totalAmount;
                sale.FinalAmount = totalAmount * (1 - (dto.DiscountPercent / 100));

                _context.Sales.Add(sale);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                _logger.LogInformation("Venta creada: {InvoiceNumber} por ${FinalAmount}",
                    sale.InvoiceNumber, sale.FinalAmount);

                return MapToDto(sale);
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<List<SaleResponseDto>> GetSalesAsync(DateTime? fromDate, DateTime? toDate)
        {
            var query = _context.Sales
                .Include(s => s.Items)
                .ThenInclude(i => i.Product)
                .AsQueryable();

            if (fromDate.HasValue)
                query = query.Where(s => s.SaleDate >= fromDate.Value);

            if (toDate.HasValue)
                query = query.Where(s => s.SaleDate <= toDate.Value);

            var sales = await query
                .OrderByDescending(s => s.SaleDate)
                .ToListAsync();

            return sales.Select(MapToDto).ToList();
        }

        public async Task<SaleResponseDto?> GetSaleByIdAsync(int id)
        {
            var sale = await _context.Sales
                .Include(s => s.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(s => s.Id == id);

            return sale == null ? null : MapToDto(sale);
        }

        private async Task<string> GenerateInvoiceNumberAsync()
        {
            var today = DateTime.UtcNow;
            var prefix = $"INV-{today:yyyyMMdd}";
            var count = await _context.Sales.CountAsync(s => s.SaleDate.Date == today.Date);
            return $"{prefix}-{(count + 1):D4}";
        }

        private static SaleResponseDto MapToDto(Sale sale)
        {
            return new SaleResponseDto
            {
                Id = sale.Id,
                InvoiceNumber = sale.InvoiceNumber,
                SaleDate = sale.SaleDate,
                TotalAmount = sale.TotalAmount,
                Discount = sale.Discount,
                FinalAmount = sale.FinalAmount,
                PaymentMethod = sale.PaymentMethod,
                CustomerName = sale.CustomerName,
                Notes = sale.Notes,
                Items = sale.Items.Select(i => new SaleItemResponseDto
                {
                    Id = i.Id,
                    ProductId = i.ProductId,
                    ProductName = i.Product?.Name ?? "N/A",
                    ProductSku = i.Product?.Sku ?? "N/A",
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                    TotalPrice = i.TotalPrice
                }).ToList()
            };
        }
    }
}