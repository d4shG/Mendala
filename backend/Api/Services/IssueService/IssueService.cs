using Api.Data.Entities;
using Api.Exceptions;
using Api.Models.Enums;
using Api.Models.IssueDtos;
using Api.Repositories.IssueRepository;

namespace Api.Services.IssueService;

public class IssueService(IIssueRepository repository) : IIssueService
{
	public async Task<IEnumerable<IssueResponseDto>> GetAllAsync()
	{
		var issues = await repository.GetAllAsync();

		return issues.Select(ConvertIssueToIssueResponseDto);
	}

	public async Task<IssueResponseDto> GetByIdAsync(Guid id)
	{
		var issue = await repository.GetByIdAsync(id);

		return ConvertIssueToIssueResponseDto(issue);
	}

	public async Task<Guid> CreateAsync(IssueCreateDto issueDto)
	{
		var issue = new Issue()
		{
			Type = issueDto.Type,
			Title = issueDto.Title,
			Description = issueDto.Description,
			InvoiceId = issueDto.InvoiceId,
			CreatorId = issueDto.CreatorId,
		};

		return await repository.CreateAsync(issue);
	}

	public async Task UpdateAsync(IssueUpdateDto issueDto)
	{
		if (IsDtoEmpty(issueDto))
			return;

		var issue = await repository.GetByIdAsync(issueDto.IssueId);
		if (issue == null)
			throw new NotFoundException($"Issue with ID {issueDto.IssueId} not found.");

		var hasChanges = false;

		if (issueDto.Type.HasValue && issue.Type != issueDto.Type.Value)
		{
			issue.Type = issueDto.Type.Value;
			hasChanges = true;
		}

		if (!string.IsNullOrWhiteSpace(issueDto.Title) && issue.Title != issueDto.Title)
		{
			issue.Title = issueDto.Title;
			hasChanges = true;
		}

		if (!string.IsNullOrWhiteSpace(issueDto.Description) && issue.Description != issueDto.Description)
		{
			issue.Description = issueDto.Description;
			hasChanges = true;
		}

		if (issueDto.InvoiceId.HasValue && issue.InvoiceId != issueDto.InvoiceId.Value)
		{
			issue.InvoiceId = issueDto.InvoiceId.Value;
			hasChanges = true;
		}

		if (!string.IsNullOrWhiteSpace(issueDto.Notes) && issue.Notes != issueDto.Notes)
		{
			issue.Notes = issueDto.Notes;
			hasChanges = true;
		}

		if (hasChanges)
			await repository.UpdateAsync(issue);
	}

	public async Task DeleteAsync(Guid id)
	{
		var issue = await repository.GetByIdAsync(id);
		if (issue == null)
			throw new NotFoundException($"Issue with ID {id} not found.");
		
		await repository.DeleteAsync(issue);
	}


	public async Task<IEnumerable<IssueResponseDto>> GetByFilterAsync(IssueFilterDto filter)
	{
		if (IsDtoEmpty(filter))
			return [];

		var issues = await repository.FilterAsync(filter);

		return issues.Select(ConvertIssueToIssueResponseDto);
	}

	public async Task UpdateStatusAsync(Guid id, IssueStatusUpdateDto status)
	{
		var issue = await repository.GetByIdAsync(id);
		if (issue == null)
			throw new NotFoundException($"Issue with ID {id} not found.");

		var hasChanges = false;

		if (issue.Status != status.NewStatus)
		{
			issue.Status = status.NewStatus;
			hasChanges = true;
		}

		if (issue.Notes != status.Notes)
		{
			issue.Notes = status.Notes;
			hasChanges = true;
		}

		if (hasChanges)
			await repository.UpdateAsync(issue);
	}

	private IssueResponseDto ConvertIssueToIssueResponseDto(Issue issue) =>
		new IssueResponseDto(
			issue.Id,
			issue.Title,
			issue.Description,
			issue.CreatorId,
			issue.Creator.UserName ?? "Admin",
			issue.InvoiceId,
			issue.Invoice.InvoiceNumber,
			issue.Invoice.CustomerId,
			issue.Invoice.Customer.Name,
			issue.Status,
			issue.Notes ?? null,
			issue.CreatedAt
		);

	private bool IsDtoEmpty<T>(T dto) where T : class
	{
		if (dto == null)
			throw new ArgumentNullException(nameof(dto));

		return typeof(T)
			.GetProperties()
			.All(p => p.GetValue(dto) == null);
	}
}