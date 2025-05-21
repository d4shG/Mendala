using Api.Models.Enums;
using Api.Models.IssueDtos;

namespace Api.Services.IssueService;

public interface IIssueService
{
	Task<List<IssueResponseDto>> GetAllAsync();
	Task<IssueResponseDto> GetByIdAsync(Guid id);
	Task<Guid> CreateAsync(IssueCreateDto issue);
	Task UpdateAsync(IssueUpdateDto issue);
	Task DeleteAsync(Guid id);
	Task<List<IssueResponseDto>> GetByFilterAsync(IssueFilterDto filter);
	Task UpdateStatusAsync(Guid id, IssueStatusUpdateDto status);
}