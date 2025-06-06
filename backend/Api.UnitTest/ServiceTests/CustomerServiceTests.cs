using Api.Data.Entities;
using Api.Exceptions;
using Api.Models.Address;
using Api.Repositories.CustomerRepository;
using Api.Services.CustomerService;
using Moq;

namespace Api.UnitTest.ServiceTests;

[TestFixture]
public class CustomerServiceTests
{
	private Mock<ICustomerRepository> _repositoryMock;
	private CustomerService _customerService;

	[SetUp]
	public void Setup()
	{
		_repositoryMock = new Mock<ICustomerRepository>();
		_customerService = new CustomerService(_repositoryMock.Object);
	}

	private Customer GetSampleCustomer(Guid id, string email)
	{
		return new Customer
		{
			Id = id,
			Name = "Test Customer",
			Email = email,
			Phone = "123-456-7890",
			Address = new Address()
			{
				StreetAddress = "123 Main St",
				City = "Test",
				PostalCode = "123",
				Country = "Test",
                
			},
		};
	}
	
	[Test]
	public async Task GetByCustomerIdAsync_CustomerFound_ReturnsCustomerResponseDto()
	{
		var customerId = Guid.NewGuid();
		var customer = GetSampleCustomer(customerId, "test@example.com");

		_repositoryMock
			.Setup(r => r.GetByCustomerIdAsync(customerId))
			.ReturnsAsync(customer);
		
		var result = await _customerService.GetByCustomerIdAsync(customerId);
		
		Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.Id, Is.EqualTo(customerId));
            Assert.That(result.Name, Is.EqualTo(customer.Name));
        });
        _repositoryMock.Verify(r => r.GetByCustomerIdAsync(customerId), Times.Once);
	}
	
	[Test]
	public void GetByCustomerIdAsync_CustomerNotFound_ThrowsNotFoundException()
	{
		var customerId = Guid.NewGuid();

		_repositoryMock
			.Setup(r => r.GetByCustomerIdAsync(customerId))
			.ReturnsAsync((Customer)null);
		
		var exception = Assert.ThrowsAsync<NotFoundException>(() => _customerService.GetByCustomerIdAsync(customerId));
		Assert.That(exception.Message, Is.EqualTo($"Product with ID {customerId} not found."));
		_repositoryMock.Verify(r => r.GetByCustomerIdAsync(customerId), Times.Once);
	}
	
	[Test]
	public async Task GetByEmailAsync_CustomersFound_ReturnsCustomerResponseDtos()
	{
		var email = "test@example.com";
		var customers = new List<Customer>
		{
			GetSampleCustomer(Guid.NewGuid(), email),
			GetSampleCustomer(Guid.NewGuid(), email)
		};

		_repositoryMock
			.Setup(r => r.GetByEmailAsync(email))
			.ReturnsAsync(customers);
		
		var result = await _customerService.GetByEmailAsync(email);
		
		Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.Count(), Is.EqualTo(customers.Count));
            Assert.That(result.All(dto => dto.Email == email));
        });
        _repositoryMock.Verify(r => r.GetByEmailAsync(email), Times.Once);
	}
	
	[Test]
	public async Task GetByEmailAsync_NoCustomersFound_ReturnsEmptyCollection()
	{
		var email = "nonexistent@example.com";

		_repositoryMock
			.Setup(r => r.GetByEmailAsync(email))
			.ReturnsAsync(new List<Customer>());
		
		var result = await _customerService.GetByEmailAsync(email);
		
		Assert.That(result, Is.Not.Null);
		Assert.That(result, Is.Empty);
		_repositoryMock.Verify(r => r.GetByEmailAsync(email), Times.Once);
	}




}