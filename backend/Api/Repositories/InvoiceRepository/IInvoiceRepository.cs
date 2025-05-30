using Api.Data.Entities;

namespace Api.Repositories.InvoiceRepository;

public interface IInvoiceRepository
{
	Task<Invoice?> GetByIdAsync(Guid id);
	Task<IEnumerable<Invoice>> GetByCustomerId(Guid id);
}