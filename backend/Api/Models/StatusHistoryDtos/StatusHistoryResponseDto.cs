using Api.Models.Enums;

namespace Api.Models.StatusHistoryDtos;

public record StatusHistoryResponseDto(
	IssueStatus Status,
	DateTime ChangedAt
	);