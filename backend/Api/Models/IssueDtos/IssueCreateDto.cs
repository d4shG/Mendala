using Api.Models.Enums;

namespace Api.Models.IssueDtos;

public record IssueCreateDto(
	IssueType Type,
	string Title,
	string Description,
	Guid InvoiceId,
	Guid CreatorId
	);