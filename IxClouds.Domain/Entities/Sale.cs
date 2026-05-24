namespace IxClouds.Domain.Entities;
public class Sale
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public decimal Total { get; set; }
    public ICollection<SaleDetail> SaleDetails { get; set; } = new List<SaleDetail>();
}
