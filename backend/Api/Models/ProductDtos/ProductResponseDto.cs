namespace Api.Models.ProductDtos;

public record ProductResponseDto(
	Guid Id, 
	string Name,
	string Sku);