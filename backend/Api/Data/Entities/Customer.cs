using System.ComponentModel.DataAnnotations;
using Api.Models.Address;

namespace Api.Data.Entities;

public class Customer
{
	public Guid Id { get; set; }
	[Required, MaxLength(50)] public string Name { get; set; }

	[Required, MaxLength(254), EmailAddress(ErrorMessage = "Invalid email address format.")]
	public string Email { get; set; }

	[Required, MaxLength(50),
	 RegularExpression(@"^\+36\s?([1-9]\d)\s?\d{3}\s?\d{4}$",
		 ErrorMessage = "Invalid Hungarian phone number format. Example: +36 1 234 5678")]
	public string Phone { get; set; }

	[Required] public Address Address { get; set; }
	
	public string? UserId { get; set; }
	public User User { get; set; }
	
	public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
}