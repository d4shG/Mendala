using Api.Data.Entities;

namespace Api.Repositories.CustomerRepository;

public interface ICustomerRepository
{
	Task<Customer?> GetByCustomerIdAsync(Guid customerId);
	Task<IEnumerable<Customer>> GetByEmailAsync(string email);
}