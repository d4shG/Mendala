using Api.Models.Enums;

namespace Api.Models.IssueDtos;

public record IssueStatusUpdateDto(
	IssueStatus NewStatus,
	string? Notes
);