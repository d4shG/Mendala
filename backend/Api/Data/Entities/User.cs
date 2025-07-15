using Api.Models.AuditableEntity;
using Microsoft.AspNetCore.Identity;

namespace Api.Data.Entities;

public class User : IdentityUser, IAuditable
{
	public DateTime CreatedAt { get; set; }
	public DateTime UpdatedAt { get; set; }
	
	public ICollection<Issue> Issues { get; set; } = new List<Issue>();
	public ICollection<Customer> Customers { get; set; } = new List<Customer>();

}