using Api.Data.Context;
using Api.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories.InvoiceRepository;

public class InvoiceRepository(MendalaApiContext context) : IInvoiceRepository
{
	public async Task<Invoice?> GetByIdAsync(Guid id) =>
		await context.Invoices
			.Include(i => i.InvoiceItems)
				.ThenInclude(ii => ii.Product)
			.SingleOrDefaultAsync(invoice => invoice.Id == id);

	public async Task<IEnumerable<Invoice>> GetByCustomerId(Guid id) =>
		await context.Invoices
			.Include(i => i.InvoiceItems)
			.ThenInclude(ii => ii.Product)
			.Where(invoice => invoice.CustomerId == id)
			.ToListAsync();
}