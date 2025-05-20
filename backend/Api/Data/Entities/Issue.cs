using System.ComponentModel.DataAnnotations;
using Api.Models.AuditableEntity;
using Api.Models.Enums;

namespace Api.Data.Entities;

public class Issue : IAuditable
{
	public Guid Id { get; set; }
	public IssueType Type { get; set; }
	public IssueStatus Status { get; set; } = IssueStatus.Received;

	[Required, MaxLength(100), MinLength(10)]
	public string Title { get; set; }

	[Required, MaxLength(1000), MinLength(50)]
	public string Description { get; set; }

	public string? Response { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime UpdatedAt { get; set; }

	[Required] public Guid InvoiceId { get; set; }
	public Invoice Invoice { get; set; }

	[Required] public Guid CreatorId { get; set; }
	public User Creator { get; set; }
	
	public ICollection<IssueStatusHistory> StatusHistory { get; set; } = new List<IssueStatusHistory>();
}