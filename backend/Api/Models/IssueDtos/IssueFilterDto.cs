using Api.Models.Enums;

namespace Api.Models.IssueDtos;

public record IssueFilterDto(
	Guid? CreatorId,
	Guid? CustomerId,
	IssueStatus? Status,
	DateTime? CreatedAfter,
	DateTime? CreatedBefore,
	DateTime? UpdatedAfter,
	DateTime? UpdatedBefore,
	string? SearchTerm);