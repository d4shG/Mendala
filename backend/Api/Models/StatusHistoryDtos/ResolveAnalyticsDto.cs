namespace Api.Models.StatusHistoryDtos;

public record ResolveAnalyticsDto(
	int ResolvedCount,
	int UnresolvedCount,
	double AverageResolveTime,
	double ResolvePercentage
);