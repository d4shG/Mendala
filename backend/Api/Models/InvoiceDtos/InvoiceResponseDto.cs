using Api.Models.InvoiceItemDtos;

namespace Api.Models.InvoiceDtos;

public record InvoiceResponseDto(
	Guid Id,
	string InvoiceNumber,
	string Name,
	Address.Address Address,
	Guid CustomerId,
	IEnumerable<InvoiceItemResponseDto> Items
	);