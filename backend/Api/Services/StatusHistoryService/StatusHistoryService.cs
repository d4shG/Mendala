using Api.Data.Entities;
using Api.Models.Enums;
using Api.Models.StatusHistoryDtos;
using Api.Repositories.StatusHistoryRepository;
using Api.Utils.DtoMapper;

namespace Api.Services.StatusHistoryService;

public class StatusHistoryService(IStatusHistoryRepository repository) : IStatusHistoryService
{
	public async Task<ResolveAnalyticsDto> GetResolveAnalyticsAsync()
	{
		var history = await repository.GetAllAsync();
		var groupedByIssue = GroupHistoryByIssue(history);

		
		var (resolvedCount, unresolvedCount, totalResolveTime) = CalculateIssueResolutionStats(groupedByIssue);
		var avgResolveTime = CalculateAverageResolveTime(resolvedCount, totalResolveTime);
		var resolvePercentage = CalculateResolvePercentage(resolvedCount, unresolvedCount);

		return new ResolveAnalyticsDto(
			resolvedCount,
			unresolvedCount,
			avgResolveTime,
			resolvePercentage);
	}


	public async Task<IEnumerable<StatusHistoryResponseDto>> GetByIssueIdAsync(Guid issueId)
	{
		var history = await repository.GetByIdAsync(issueId);

		return history.Select(DtoMapper.ConvertIssueStatusHistoryToStatusHistoryResponseDto);
	}

	private IEnumerable<IGrouping<Guid, IssueStatusHistory>> GroupHistoryByIssue(IEnumerable<IssueStatusHistory> history)
	{
		return history.GroupBy(i => i.IssueId);
	}
	
	
	private (int resolvedCount, int unresolvedCount, double totalResolveTime) CalculateIssueResolutionStats(IEnumerable<IGrouping<Guid, IssueStatusHistory>> groupedByIssue)
	{
		var resolvedCount = 0;
		var unresolvedCount = 0;
		var totalResolveTime = 0.0;

		foreach (var group in groupedByIssue)
		{
			var completedEntry = group.FirstOrDefault(i => i.Status == IssueStatus.Completed);
			var receivedEntry = group.FirstOrDefault(i => i.Status == IssueStatus.Received);

			if (completedEntry != null && receivedEntry != null)
			{
				resolvedCount++;
				totalResolveTime += (completedEntry.ChangedAt - receivedEntry.ChangedAt).TotalDays;
			}
			else
			{
				unresolvedCount++;
			}
		}

		return (resolvedCount, unresolvedCount, totalResolveTime);
	}



	
	private double CalculateAverageResolveTime(int resolvedCount, double totalResolveTime)
	{
		return resolvedCount > 0 ? totalResolveTime / resolvedCount : 0;
	}
	
	private double CalculateResolvePercentage(int resolvedCount, int unresolvedCount)
	{
		var totalCount = resolvedCount + unresolvedCount;
		return totalCount > 0 ? (double)resolvedCount / totalCount * 100 : 0;
	}




}