using Api.Data.Context;
using Api.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories.CustomerRepository;

public class CustomerRepository(MendalaApiContext context) : ICustomerRepository
{
	public async Task<Customer?> GetByCustomerIdAsync(Guid customerId) =>
	 await context.Customers.SingleOrDefaultAsync(c => c.Id == customerId);

	public async Task<IEnumerable<Customer>> GetByEmailAsync(string email) =>
	 await context.Customers.Where(c => c.Email == email).ToListAsync();
}