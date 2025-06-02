using Api.Data.Context;
using Api.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories.StatusHistoryRepository;

public class StatusHistoryRepository(MendalaApiContext context) : IStatusHistoryRepository
{
	public async Task<IEnumerable<IssueStatusHistory>> GetAllAsync() =>
		await context.IssueStatusHistory.ToListAsync();


	public async Task<IEnumerable<IssueStatusHistory>> GetByIdAsync(Guid issueId) =>
		await context.IssueStatusHistory.Where(i => i.IssueId == issueId).ToListAsync();
}