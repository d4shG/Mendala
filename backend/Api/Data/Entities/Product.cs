using System.ComponentModel.DataAnnotations;

namespace Api.Data.Entities;

public class Product
{
	public Guid Id { get; set; }
	[Required, MaxLength(100)] public string Name { get; set; }
	[Required, MaxLength(50)] public string Sku { get; set; }

	public ICollection<InvoiceItem> InvoiceItems { get; set; } = new List<InvoiceItem>();
}