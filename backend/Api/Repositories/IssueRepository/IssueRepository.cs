using Api.Data.Context;
using Api.Data.Entities;
using Api.Models.IssueDtos;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories.IssueRepository;

public class IssueRepository(MendalaApiContext context): IIssueRepository
{
	public async Task<IEnumerable<Issue>> GetAllAsync() =>
		await context.Issues
			.Include(i => i.Creator)
			.Include(i => i.Invoice)
				.ThenInclude(inv => inv.Customer)
			.ToListAsync();

	public async Task<Issue?> GetByIdAsync(Guid id) =>
		await context.Issues
			.Include(i => i.Creator)
			.Include(i => i.Invoice)
			.ThenInclude(inv => inv.Customer)
			.SingleOrDefaultAsync(i => i.Id == id);

	public async Task<Guid> CreateAsync(Issue issue)
	{
		context.Issues.Add(issue);
		await context.SaveChangesAsync();
		return issue.Id;
	}

	public async Task UpdateAsync(Issue issue)
	{
		context.Issues.Update(issue);
		await context.SaveChangesAsync();
	}

	public async Task DeleteAsync(Issue issue)
	{
		context.Issues.Remove(issue);
		await context.SaveChangesAsync();
	}

	public async Task<IEnumerable<Issue>> FilterAsync(IssueFilterDto filterDto)
	{
		var query = context.Issues.AsQueryable();

		if (filterDto.CreatorId.HasValue)
			query = query.Where(i => i.CreatorId == filterDto.CreatorId);

		if (filterDto.CustomerId.HasValue)
			query = query.Where(i => i.Invoice.CustomerId == filterDto.CustomerId);

		if (filterDto.Status.HasValue)
			query = query.Where(i => i.Status == filterDto.Status);

		if (filterDto.CreatedAfter.HasValue)
			query = query.Where(i => i.CreatedAt >= filterDto.CreatedAfter);

		if (filterDto.CreatedBefore.HasValue)
			query = query.Where(i => i.CreatedAt <= filterDto.CreatedBefore);

		if (filterDto.UpdatedAfter.HasValue)
			query = query.Where(i => i.UpdatedAt >= filterDto.UpdatedAfter);

		if (filterDto.UpdatedBefore.HasValue)
			query = query.Where(i => i.UpdatedAt <= filterDto.UpdatedBefore);

		if (!string.IsNullOrEmpty(filterDto.SearchTerm))
		{
			var lowerTerm = filterDto.SearchTerm.ToLower();
			query = query.Where(i => i.Title.ToLower().Contains(lowerTerm));
		}

		return await query.ToListAsync();
	}
}