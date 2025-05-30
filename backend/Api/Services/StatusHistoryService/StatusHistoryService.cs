using Api.Models.StatusHistoryDtos;

namespace Api.Services.StatusHistoryService;

public class StatusHistoryService(IStatusHistoryRepository repository) : IStatusHistoryService
{
	public Task<double> GetAverageTimeToResolveAsync()
	{
		throw new NotImplementedException();
	}

	public Task<int> GetStatusCountsAsync()
	{
		throw new NotImplementedException();
	}

	public Task<IEnumerable<StatusHistoryResponseDto>> GetByIssueIdAsync(Guid issueId)
	{
		throw new NotImplementedException();
	}
}