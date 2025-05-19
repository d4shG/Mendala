using System.ComponentModel.DataAnnotations;

namespace Api.Models.Address;

public class Address
{
	[Required, MaxLength(100)]
	public string StreetAddress { get; set; }
	[Required, MaxLength(50)]
	public string City { get; set; }
	[Required, MaxLength(50)]
	public string PostalCode { get; set; }
	[Required, MaxLength(50)]
	public string Country { get; set; }

}