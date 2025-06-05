using Api.Services.InvoiceService;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.InvoiceController;

[ApiController]
[Route("[controller]")]
public class InvoiceController(IInvoiceService service): ControllerBase
{
	[HttpGet("get-by-invoice-id/{invoiceId:guid}")]
	public async Task<IActionResult> GetByInvoiceId(Guid invoiceId)
	{
		var invoice = await service.GetInvoiceByIdAsync(invoiceId);
		
		return Ok(invoice);
	}
	
	[HttpGet("get-by-customer-id/{customerId:guid}")]
	public async Task<IActionResult> GetByCustomerIdAsync(Guid customerId)
	{
		var invoices = await service.GetByCustomerId(customerId);
		
		return Ok(invoices);
	}
	
}