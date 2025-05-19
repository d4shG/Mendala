using System.ComponentModel.DataAnnotations;
using Api.Models.Address;

namespace Api.Data.Entities;

public class Invoice
{
	public Guid Id { get; set; }
	[Required, MaxLength(50)] public string InvoiceNumber { get; set; }
	[Required, MaxLength(100)] public string Name { get; set; }
	[Required] public Address Address { get; set; }

	[Required] public Guid CustomerId { get; set; }
	[Required] public Customer Customer { get; set; }

	public ICollection<Issue> Issues { get; set; } = new List<Issue>();
	public ICollection<InvoiceItem> InvoiceItems { get; set; } = new List<InvoiceItem>();
	
}