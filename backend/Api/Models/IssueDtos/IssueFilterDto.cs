using Api.Models.Enums;

namespace Api.Models.IssueDtos;

public record IssueFilterDto(
	string? CreatorId,
	Guid? CustomerId,
	IssueStatus? Status,
	DateTime? CreatedAfter,
	DateTime? CreatedBefore,
	DateTime? UpdatedAfter,
	DateTime? UpdatedBefore,
	string? SearchTerm);