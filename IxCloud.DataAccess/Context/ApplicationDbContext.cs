using IxClouds.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace IxCloud.DataAccess.Context;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    public DbSet<Product> Products { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<SaleDetail> SaleDetails { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Brand).IsRequired().HasMaxLength(100);
            entity.Property(e => e.PhoneModel).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Gender).HasMaxLength(20);
            entity.Property(e => e.Material).HasMaxLength(50);
            entity.Property(e => e.Stock).HasDefaultValue(0);
            entity.Property(e => e.PurchasePrice).HasPrecision(18, 2);
            entity.Property(e => e.SalePrice).HasPrecision(18, 2);
            entity.Property(e => e.ImageUrl).HasMaxLength(500);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");
        });
        modelBuilder.Entity<Sale>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Date).HasDefaultValueSql("GETDATE()");
            entity.Property(e => e.Total).HasPrecision(18, 2);
        });
        modelBuilder.Entity<SaleDetail>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.UnitPrice).HasPrecision(18, 2);
            entity.Property(e => e.Subtotal).HasPrecision(18, 2);
            entity.HasOne(d => d.Sale).WithMany(s => s.SaleDetails).HasForeignKey(d => d.SaleId).OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(d => d.Product).WithMany(p => p.SaleDetails).HasForeignKey(d => d.ProductId).OnDelete(DeleteBehavior.Restrict);
        });
        modelBuilder.Entity<Product>().HasIndex(p => p.Brand);
        modelBuilder.Entity<Product>().HasIndex(p => p.PhoneModel);
        modelBuilder.Entity<Product>().HasIndex(p => p.Material);
        modelBuilder.Entity<Sale>().HasIndex(s => s.Date);
    }
}
