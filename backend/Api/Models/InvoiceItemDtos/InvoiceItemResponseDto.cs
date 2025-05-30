using Api.Models.ProductDtos;

namespace Api.Models.InvoiceItemDtos;

public record InvoiceItemResponseDto(
	Guid Id, 
	ProductResponseDto Product,
	decimal Price,
	int Quantity
);