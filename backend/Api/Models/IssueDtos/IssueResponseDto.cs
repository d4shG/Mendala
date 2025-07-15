using Api.Models.Enums;

namespace Api.Models.IssueDtos;

public record IssueResponseDto(
	Guid Id,
	string Title,
	string Description,
	string CreatorId,
	string CreatorName,
	Guid CustomerId,
	string CustomerName,
	Guid InvoiceId,
	string InvoiceNumber,
	string Status,
	string Type,
	string? Notes,
	DateTime CreatedAt);
