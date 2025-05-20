using Api.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.IssueController;

[ApiController]
[Route("[controller]")]
public class IssueController : ControllerBase
{
	[HttpGet]
	public async Task<IActionResult> GetIssues()
	{
		throw new NotImplementedException();
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetIssueById(Guid id)
	{
		throw new NotImplementedException();
	}

	[HttpPost]
	public async Task<IActionResult> Create([FromBody] IssueCreateDTo issue)
	{
		throw new NotImplementedException();
	}

	[HttpPut]
	public async Task<IActionResult> Update([FromBody] IssueUpdateDto issue)
	{
		throw new NotImplementedException();
	}

	[HttpDelete("{id}")]
	public IActionResult Delete(Guid id)
	{
		throw new NotImplementedException();
	}
}