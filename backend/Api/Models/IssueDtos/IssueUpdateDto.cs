using Api.Models.Enums;

namespace Api.Models.IssueDtos;

public record IssueUpdateDto(
	Guid IssueId,
	IssueType? Type,
	string? Title,
	string? Description,
	Guid? InvoiceId,
	string? Notes
	);