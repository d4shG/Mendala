namespace Api.Models.InvoiceItemDtos;

public record InvoiceItemResponseDto(
	Guid Id, 
	Guid ProductId,
	string Name,
	string Sku,
	decimal Price,
	int Quantity
);