using Api.Models.Enums;

namespace Api.Models.IssueDtos;

public record IssueResponseDto(
	Guid Id,
	string Title,
	string Description,
	Guid CreatorId,
	string CreatorName,
	Guid CustomerId,
	string CustomerName,
	Guid InvoiceId,
	string InvoiceNumber,
	IssueStatus Status,
	string? Notes,
	DateTime CreatedAt);
