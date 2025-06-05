using Api.Services.CustomerService;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.CustomerController;

[ApiController]
[Route("[controller]")]
public class CustomerController(ICustomerService service): ControllerBase
{
	[HttpGet("get-by-id/{customerId:guid}")]
	public async Task<IActionResult> GetById(Guid customerId)
	{
		var customer = await service.GetByCustomerIdAsync(customerId);
		
		return Ok(customer);
	}

	[HttpGet("get-by-email/{email}")]
	public async Task<IActionResult> GetByEmail(string email)
	{
		var customers = await service.GetByEmailAsync(email);
		
		return Ok(customers);
	}
}