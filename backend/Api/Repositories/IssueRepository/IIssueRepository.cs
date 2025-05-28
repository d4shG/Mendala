using Api.Data.Entities;
using Api.Models.IssueDtos;

namespace Api.Repositories.IssueRepository;

public interface IIssueRepository
{
	Task<IEnumerable<Issue>> GetAllAsync();
	Task<Issue?> GetByIdAsync(Guid id);
	Task<Guid> CreateAsync(Issue issue);
	Task UpdateAsync(Issue issue);
	Task DeleteAsync(Issue issue);
	Task<IEnumerable<Issue>> FilterAsync(IssueFilterDto filterDto);
}