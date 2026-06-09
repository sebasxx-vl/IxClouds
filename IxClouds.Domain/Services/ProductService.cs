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
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProductService> _logger;

        public ProductService(ApplicationDbContext context, ILogger<ProductService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ProductResponseDto> CreateAsync(CreateProductRequestDto dto)
        {
            if (await _context.Products.AnyAsync(p => p.Sku == dto.Sku))
                throw new InvalidOperationException($"Ya existe un producto con el SKU: {dto.Sku}");

            var product = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Sku = dto.Sku,
                Price = dto.Price,
                Cost = dto.Cost,
                StockQuantity = dto.StockQuantity,
                MinStockLevel = dto.MinStockLevel,
                Category = dto.Category,
                ImageUrl = dto.ImageUrl,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Producto creado: {Sku} - {Name}", product.Sku, product.Name);

            return MapToDto(product);
        }

        public async Task<ProductResponseDto> UpdateAsync(int id, UpdateProductRequestDto dto)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                throw new KeyNotFoundException($"Producto ID {id} no encontrado");

            if (await _context.Products.AnyAsync(p => p.Sku == dto.Sku && p.Id != id))
                throw new InvalidOperationException($"Ya existe otro producto con el SKU: {dto.Sku}");

            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Sku = dto.Sku;
            product.Price = dto.Price;
            product.Cost = dto.Cost;
            product.StockQuantity = dto.StockQuantity;
            product.MinStockLevel = dto.MinStockLevel;
            product.Category = dto.Category;
            product.ImageUrl = dto.ImageUrl;
            product.IsActive = dto.IsActive;
            product.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return MapToDto(product);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return false;

            product.IsActive = false;
            product.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<ProductResponseDto?> GetByIdAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            return product == null ? null : MapToDto(product);
        }

        public async Task<PaginatedResponse<ProductResponseDto>> SearchAsync(SearchProductRequestDto filter)
        {
            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.SearchTerm))
            {
                var term = filter.SearchTerm.ToLower();
                query = query.Where(p =>
                    p.Name.ToLower().Contains(term) ||
                    p.Sku.ToLower().Contains(term) ||
                    p.Description.ToLower().Contains(term));
            }

            if (!string.IsNullOrWhiteSpace(filter.Category))
                query = query.Where(p => p.Category == filter.Category);

            if (filter.MinPrice.HasValue)
                query = query.Where(p => p.Price >= filter.MinPrice.Value);

            if (filter.MaxPrice.HasValue)
                query = query.Where(p => p.Price <= filter.MaxPrice.Value);

            if (filter.LowStockOnly.HasValue && filter.LowStockOnly.Value)
                query = query.Where(p => p.StockQuantity <= p.MinStockLevel);

            if (filter.IsActive.HasValue)
                query = query.Where(p => p.IsActive == filter.IsActive.Value);

            query = filter.SortBy?.ToLower() switch
            {
                "price" => filter.SortDescending ? query.OrderByDescending(p => p.Price) : query.OrderBy(p => p.Price),
                "stock" => filter.SortDescending ? query.OrderByDescending(p => p.StockQuantity) : query.OrderBy(p => p.StockQuantity),
                "date" => filter.SortDescending ? query.OrderByDescending(p => p.CreatedAt) : query.OrderBy(p => p.CreatedAt),
                _ => filter.SortDescending ? query.OrderByDescending(p => p.Name) : query.OrderBy(p => p.Name)
            };

            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            return new PaginatedResponse<ProductResponseDto>
            {
                Items = items.Select(MapToDto).ToList(),
                TotalCount = totalCount,
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize
            };
        }

        private static ProductResponseDto MapToDto(Product product)
        {
            return new ProductResponseDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Sku = product.Sku,
                Price = product.Price,
                Cost = product.Cost,
                StockQuantity = product.StockQuantity,
                MinStockLevel = product.MinStockLevel,
                Category = product.Category,
                ImageUrl = product.ImageUrl,
                IsActive = product.IsActive,
                HasLowStock = product.StockQuantity <= product.MinStockLevel,
                ProfitMargin = product.Price > 0 ? ((product.Price - product.Cost) / product.Price) * 100 : 0,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt
            };
        }
    }
}