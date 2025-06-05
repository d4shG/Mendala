using Api.Data.Entities;
using Api.Models.IssueDtos;
using Api.Services.IssueService;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.IssueController;

[ApiController]
[Route("[controller]")]
public class IssueController(IIssueService issueService) : ControllerBase
{
	[HttpGet]
	public async Task<IActionResult> GetIssues()
	{
		var issues = await issueService.GetAllAsync();
		
		return Ok(issues);
	}

	[HttpGet("get-by-id/{id:guid}")]
	public async Task<IActionResult> GetIssueById(Guid id)
	{
		var issue = await issueService.GetByIdAsync(id);
		
		if (issue == null)
			return NotFound();
		
		return Ok(issue);
	}

	[HttpPost]
	public async Task<IActionResult> Create([FromBody] IssueCreateDto issue)
	{
		var createdIssueId = await issueService.CreateAsync(issue);

		return CreatedAtAction(nameof(GetIssueById), new { id = createdIssueId });
	}

	[HttpPut]
	public async Task<IActionResult> Update([FromBody] IssueUpdateDto issue)
	{
		await issueService.UpdateAsync(issue);

		return NoContent();
	}

	[HttpDelete("{id:guid}")]
	public async Task<IActionResult> Delete(Guid id)
	{
		await issueService.DeleteAsync(id);

		return NoContent();
	}
	
	[HttpGet("filter")]
	public async Task<IActionResult> GetIssuesByFilter([FromQuery] IssueFilterDto filter)
	{
		var issues = await issueService.GetByFilterAsync(filter);
		return Ok(issues);
	}
	
	[HttpPatch("status/{id:guid}")]
	public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] IssueStatusUpdateDto statusUpdateDto)
	{
		await issueService.UpdateStatusAsync(id, statusUpdateDto);
		return Ok();
	}
}