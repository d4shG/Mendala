using System.ComponentModel.DataAnnotations;

namespace Api.Data.Entities;

public class InvoiceItem
{
	public Guid Id { get; set; }

	[Required] public Guid InvoiceId { get; set; }

	[Required] public Invoice Invoice { get; set; }

	[Required,
	 Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
	public int Quantity { get; set; }

	[Required,
	 Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
	public decimal PriceAtPurchase { get; set; }

	[Required] public Guid ProductId { get; set; }
	[Required] public Product Product { get; set; }
}