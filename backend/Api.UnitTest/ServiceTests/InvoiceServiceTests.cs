using Api.Data.Entities;
using Api.Exceptions;
using Api.Models.Address;
using Api.Repositories.InvoiceRepository;
using Api.Services.InvoiceService;
using Moq;

namespace Api.UnitTest.ServiceTests;

[TestFixture]
public class InvoiceServiceTests
{
    private Mock<IInvoiceRepository> _repositoryMock;
    private InvoiceService _service;

    [SetUp]
    public void SetUp()
    {
        _repositoryMock = new Mock<IInvoiceRepository>();
        _service = new InvoiceService(_repositoryMock.Object);
    }

    [Test]
    public async Task GetInvoiceByIdAsync_ShouldReturnInvoiceResponseDto_WhenInvoiceExists()
    {
        var id = Guid.NewGuid();
        var invoice = new Invoice
        {
            Id = id,
            InvoiceNumber = "INV123",
            Name = "Test Invoice",
            Address = new Address()
            {
                StreetAddress = "123 Main St",
                City = "Test",
                PostalCode = "123",
                Country = "Test",
                
            },
            CustomerId = Guid.NewGuid(),
            InvoiceItems = new List<InvoiceItem>()
        };
        _repositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(invoice);

        var result = await _service.GetInvoiceByIdAsync(id);

        Assert.That(result.Id, Is.EqualTo(id));
        Assert.That(result.InvoiceNumber, Is.EqualTo(invoice.InvoiceNumber));
    }

    [Test]
    public void GetInvoiceByIdAsync_ShouldThrowNotFoundException_WhenInvoiceDoesNotExist()
    {
        var id = Guid.NewGuid();
        _repositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((Invoice)null);

        Assert.ThrowsAsync<NotFoundException>(() => _service.GetInvoiceByIdAsync(id));
    }

    [Test]
    public async Task GetByCustomerId_ShouldReturnListOfInvoiceResponseDto_WhenInvoicesExist()
    {
        var customerId = Guid.NewGuid();
        var invoices = new List<Invoice>
        {
            new Invoice { Id = Guid.NewGuid(), InvoiceNumber = "INV001", CustomerId = customerId },
            new Invoice { Id = Guid.NewGuid(), InvoiceNumber = "INV002", CustomerId = customerId }
        };
        _repositoryMock.Setup(r => r.GetByCustomerId(customerId)).ReturnsAsync(invoices);

        var result = await _service.GetByCustomerId(customerId);

        Assert.That(result.Count(), Is.EqualTo(invoices.Count));
        Assert.That(result.First().InvoiceNumber, Is.EqualTo(invoices.First().InvoiceNumber));
    }

    [Test]
    public async Task GetByCustomerId_ShouldReturnEmptyList_WhenNoInvoicesExist()
    {
        var customerId = Guid.NewGuid();
        _repositoryMock.Setup(r => r.GetByCustomerId(customerId)).ReturnsAsync(new List<Invoice>());

        var result = await _service.GetByCustomerId(customerId);

        Assert.That(result, Is.Empty);
    }
}