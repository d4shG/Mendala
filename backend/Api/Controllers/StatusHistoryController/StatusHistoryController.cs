using Api.Services.StatusHistoryService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Api.Controllers.StatusHistoryController;

[ApiController]
[Route("[controller]")]
public class StatusHistoryController(IStatusHistoryService service) : ControllerBase
{
	
	[HttpGet]
	public async Task<IActionResult> GetAverageTimeToResolve()
	{
		var avgTime = await service.GetAverageTimeToResolveAsync();
		
		return Ok(avgTime);
	}
	
	[HttpGet]
	public async Task<IActionResult> GetStatusCounts()
	{
		var statusCounts = await service.GetStatusCountsAsync();
		
		return Ok(statusCounts);
	}
	
	
	[HttpGet]
	public async Task<IActionResult> GetHistoryForIssue(Guid issueId)
	{
		var statusHistory = await service.GetByIssueIdAsync(issueId);
		
		return Ok(statusHistory);
	}

}