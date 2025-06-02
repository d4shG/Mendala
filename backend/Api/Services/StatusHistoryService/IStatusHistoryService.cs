using Api.Models.StatusHistoryDtos;

namespace Api.Services.StatusHistoryService;

public interface IStatusHistoryService
{
	Task<ResolveAnalyticsDto> GetResolveAnalyticsAsync();
	Task<IEnumerable<StatusHistoryResponseDto>> GetByIssueIdAsync(Guid issueId);
}