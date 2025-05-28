using Api.Models.Enums;
using Api.Models.IssueDtos;

namespace Api.Services.IssueService;

public interface IIssueService
{
	Task<IEnumerable<IssueResponseDto>> GetAllAsync();
	Task<IssueResponseDto> GetByIdAsync(Guid id);
	Task<Guid> CreateAsync(IssueCreateDto issueDto);
	Task UpdateAsync(IssueUpdateDto issueDto);
	Task DeleteAsync(Guid id);
	Task<IEnumerable<IssueResponseDto>> GetByFilterAsync(IssueFilterDto filter);
	Task UpdateStatusAsync(Guid id, IssueStatusUpdateDto status);
}