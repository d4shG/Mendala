using Api.Models.CustomerDtos;

namespace Api.Services.CustomerService;

public interface ICustomerService
{
	Task<CustomerResponseDto> GetByCustomerIdAsync(Guid customerId);
	Task<IEnumerable<CustomerResponseDto>> GetByEmailAsync(string email);
}