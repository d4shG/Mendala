using Api.Data.Context;
using Api.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories.ProductRepository;

public class ProductRepository(MendalaApiContext context) : IProductRepository
{
	public async Task<Product?> GetByProductIdAsync(Guid productId) =>
		await context.Products.SingleOrDefaultAsync(p => p.Id == productId);
	

	public async Task<IEnumerable<Product>> GetAllAsync() =>
	 await context.Products.ToListAsync();
}