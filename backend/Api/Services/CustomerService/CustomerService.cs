using Api.Exceptions;
using Api.Models.CustomerDtos;
using Api.Repositories.CustomerRepository;
using Api.Utils.DtoMapper;

namespace Api.Services.CustomerService;

public class CustomerService(ICustomerRepository repository) : ICustomerService
{
	public async Task<CustomerResponseDto> GetByCustomerIdAsync(Guid customerId)
	{
		var customer = await repository.GetByCustomerIdAsync(customerId);

		if (customer is null)
			throw new NotFoundException($"Product with ID {customerId} not found.");
		
		return DtoMapper.ConvertCustomerToCustomerResponseDto(customer);
	}

	public async Task<IEnumerable<CustomerResponseDto>> GetByEmailAsync(string email)
	{
		var customers = await repository.GetByEmailAsync(email);
		
		return customers.Select(DtoMapper.ConvertCustomerToCustomerResponseDto);
	}
	
}