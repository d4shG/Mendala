using System.ComponentModel.DataAnnotations;
using Api.Models.Enums;

namespace Api.Data.Entities;

public class IssueStatusHistory
{
	public Guid Id { get; set; }
	[Required] public Guid IssueId { get; set; }
	public Issue Issue { get; set; }

	public IssueStatus Status { get; set; }

	public DateTime ChangedAt { get; set; }
}