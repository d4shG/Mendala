using Api.Exceptions;
using Api.Models.ProductDtos;
using Api.Repositories.ProductRepository;
using Api.Utils.DtoMapper;

namespace Api.Services.ProductService;

public class ProductService(IProductRepository repository) : IProductService
{
	public async Task<ProductResponseDto> GetByProductIdAsync(Guid productId)
	{
		var product = await repository.GetByProductIdAsync(productId);

		if (product is null)
			throw new NotFoundException($"Product with ID {productId} not found.");
		
		return DtoMapper.ConvertProductToProductResponseDto(product);
	}

	public async Task<IEnumerable<ProductResponseDto>> GetAllAsync()
	{
		var products = await repository.GetAllAsync();
		
		return products.Select(DtoMapper.ConvertProductToProductResponseDto);
	}
	
}