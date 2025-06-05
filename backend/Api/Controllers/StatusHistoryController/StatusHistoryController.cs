using Api.Services.StatusHistoryService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Api.Controllers.StatusHistoryController;

[ApiController]
[Route("[controller]")]
public class StatusHistoryController(IStatusHistoryService service) : ControllerBase
{
	
	[HttpGet("/resolve-analytics")]
	public async Task<IActionResult> GetResolveAnalytics()
	{
		var resolveAnalytics = await service.GetResolveAnalyticsAsync();
		
		return Ok(resolveAnalytics);
	}
	
	
	[HttpGet("get-by-issue-id/{issueId:guid}")]
	public async Task<IActionResult> GetHistoryForIssue(Guid issueId)
	{
		var statusHistory = await service.GetByIssueIdAsync(issueId);
		
		return Ok(statusHistory);
	}

}