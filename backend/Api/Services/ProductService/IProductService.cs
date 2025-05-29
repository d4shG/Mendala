using Api.Models.ProductDtos;

namespace Api.Services.ProductService;

public interface IProductService
{
	Task<ProductResponseDto> GetByProductIdAsync(Guid productId);
	Task<IEnumerable<ProductResponseDto>> GetAllAsync();
}