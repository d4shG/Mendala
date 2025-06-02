using Api.Data.Entities;

namespace Api.Repositories.StatusHistoryRepository;

public interface IStatusHistoryRepository
{
	Task<IEnumerable<IssueStatusHistory>> GetAllAsync();
	Task<IEnumerable<IssueStatusHistory>> GetByIdAsync(Guid issueId);
}