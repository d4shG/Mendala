using Api.Services.InvoiceService;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.InvoiceController;

[ApiController]
[Route("[controller]")]
public class InvoiceController(IInvoiceService service): ControllerBase
{
	[HttpGet]
	public async Task<IActionResult> GetByInvoiceId(Guid invoiceId)
	{
		var invoice = await service.GetInvoiceByIdAsync(invoiceId);
		
		return Ok(invoice);
	}
	
	[HttpGet]
	public async Task<IActionResult> GetByCustomerIdAsync(Guid customerId)
	{
		var invoices = await service.GetByCustomerId(customerId);
		
		return Ok(invoices);
	}
	
}