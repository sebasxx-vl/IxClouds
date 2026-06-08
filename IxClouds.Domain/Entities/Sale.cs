using System.Net.ServerSentEvents;

namespace IxClouds.Domain.Entities;

public class Sale
{
    public int Id { get; set; }
    public string InvoiceNumber { get; set; } = string.Empty;
    public DateTime SaleDate { get; set; } = DateTime.UtcNow;
    public decimal TotalAmount { get; set; }
    public decimal Discount { get; set; }
    public decimal FinalAmount { get; set; }
    public string PaymentMethod { get; set; } = "Cash";
    public string Notes { get; set; } = string.Empty;
    public int? UserId { get; set; }
    public string CustomerName { get; set; } = "General";

    public ICollection<SaleItem> Items { get; set; } = new List<SaleItem>();
}