using Api.Exceptions;
using Api.Models.InvoiceDtos;
using Api.Repositories.InvoiceRepository;
using Api.Utils.DtoMapper;

namespace Api.Services.InvoiceService;

public class InvoiceService(IInvoiceRepository repository) : IInvoiceService
{
	public async Task<InvoiceResponseDto> GetInvoiceByIdAsync(Guid id)
	{
		var invoice = await repository.GetByIdAsync(id);

		if (invoice is null)
			throw new NotFoundException($"Invoice with ID {id} not found.");

		return DtoMapper.ConvertInvoiceToInvoiceResponseDto(invoice);
	}

	public async Task<IEnumerable<InvoiceResponseDto>> GetByCustomerId(Guid id)
	{
		var invoices = await repository.GetByCustomerId(id);
		
		return invoices.Select(DtoMapper.ConvertInvoiceToInvoiceResponseDto);
		
	}
	
}