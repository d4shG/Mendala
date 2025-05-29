using Api.Models.InvoiceDtos;

namespace Api.Services.InvoiceService;

public interface IInvoiceService
{
	Task<InvoiceResponseDto> GetInvoiceByIdAsync(Guid id);
	Task<IEnumerable<InvoiceResponseDto>> GetByCustomerId(Guid id);
}