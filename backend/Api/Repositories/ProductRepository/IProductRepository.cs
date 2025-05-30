using Api.Data.Entities;

namespace Api.Repositories.ProductRepository;

public interface IProductRepository
{
	Task<Product?> GetByProductIdAsync(Guid productId);
	Task<IEnumerable<Product>> GetAllAsync();
}