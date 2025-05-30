using Api.Models.StatusHistoryDtos;

namespace Api.Services.StatusHistoryService;

public interface IStatusHistoryService
{
	Task<double> GetAverageTimeToResolveAsync();
	Task<int> GetStatusCountsAsync();
	Task<IEnumerable<StatusHistoryResponseDto>> GetByIssueIdAsync(Guid issueId);
}