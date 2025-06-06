using Api.Data.Entities;
using Api.Models.Address;
using Api.Models.Enums;
using Api.Utils.DtoMapper;

namespace Api.UnitTest.UtilsTests;

using NUnit.Framework;

[TestFixture]
public class DtoMapperTests
{
    [Test]
    public void ConvertProductToProductResponseDto_ValidProduct_ReturnsExpectedDto()
    {
        var product = new Product
        {
            Id = Guid.Empty,
            Name = "Test Product",
            Sku = "SKU123"
        };
        
        var result = DtoMapper.ConvertProductToProductResponseDto(product);
        
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.Id, Is.EqualTo(product.Id));
            Assert.That(result.Name, Is.EqualTo(product.Name));
            Assert.That(result.Sku, Is.EqualTo(product.Sku));
        });
    }

    [Test]
    public void ConvertIssueToIssueResponseDto_ValidIssue_ReturnsExpectedDto()
    {

        var issue = new Issue
        {
            Id = Guid.Empty,
            Title = "Test Issue",
            Description = "Description here",
            CreatorId = "1id",
            Creator = new User { UserName = "AdminUser" },
            InvoiceId = Guid.Empty,
            Invoice = new Invoice
            {
                InvoiceNumber = "INV001",
                CustomerId = Guid.Empty,
                Customer = new Customer { Name = "Test Customer" }
            },
            Status = IssueStatus.Diagnosed,
            Notes = "Some notes",
            CreatedAt = DateTime.Now
        };
        
        var result = DtoMapper.ConvertIssueToIssueResponseDto(issue);
        
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.Id, Is.EqualTo(issue.Id));
            Assert.That(result.Title, Is.EqualTo(issue.Title));
            Assert.That(result.CreatorName, Is.EqualTo(issue.Creator.UserName));
            Assert.That(result.InvoiceNumber, Is.EqualTo(issue.Invoice.InvoiceNumber));
        });
    }
    
    [Test]
    public void ConvertIssueToIssueResponseDto_ValidIssue_WithEmptyCreatorName_ReturnsExpectedDto()
    {

        var issue = new Issue
        {
            Id = Guid.Empty,
            Title = "Test Issue",
            Description = "Description here",
            CreatorId = "1id",
            Creator = new User(),
            InvoiceId = Guid.Empty,
            Invoice = new Invoice
            {
                InvoiceNumber = "INV001",
                CustomerId = Guid.Empty,
                Customer = new Customer { Name = "Test Customer" }
            },
            Status = IssueStatus.Diagnosed,
            Notes = "Some notes",
            CreatedAt = DateTime.Now
        };
        const string expectedCreatorName = "Admin";
        
        var result = DtoMapper.ConvertIssueToIssueResponseDto(issue);
        
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.Id, Is.EqualTo(issue.Id));
            Assert.That(result.Title, Is.EqualTo(issue.Title));
            Assert.That(issue.Creator.UserName, Is.Null);
            Assert.That(result.CreatorName, Is.EqualTo(expectedCreatorName));
            Assert.That(result.InvoiceNumber, Is.EqualTo(issue.Invoice.InvoiceNumber));
        });
    }

    [Test]
    public void ConvertCustomerToCustomerResponseDto_ValidCustomer_ReturnsExpectedDto()
    {

        var customer = new Customer
        {
            Id = Guid.Empty,
            Name = "John Doe",
            Email = "john@example.com",
            Phone = "123456789",
            Address = new Address()
            {
                StreetAddress = "123 Main St",
                City = "Test",
                PostalCode = "123",
                Country = "Test",
                
            }
        };
        
        var result = DtoMapper.ConvertCustomerToCustomerResponseDto(customer);
        
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.Name, Is.EqualTo(customer.Name));
            Assert.That(result.Email, Is.EqualTo(customer.Email));
        });
    }

    [Test]
    public void ConvertInvoiceToInvoiceResponseDto_ValidInvoice_ReturnsExpectedDto()
    {

        var product = new Product { Id = Guid.Empty, Name = "Product 1", Sku = "SKU1" };
        var invoiceItem = new InvoiceItem
        {
            Id = Guid.Empty,
            Product = product,
            PriceAtPurchase = 100.0m,
            Quantity = 2
        };
        var invoice = new Invoice
        {
            Id = Guid.Empty,
            InvoiceNumber = "INV001",
            Name = "Invoice Name",
            Address = new Address()
            {
                StreetAddress = "123 Main St",
                City = "Test",
                PostalCode = "123",
                Country = "Test",
                
            },
            CustomerId = Guid.Empty,
            InvoiceItems = new List<InvoiceItem> { invoiceItem }
        };
        
        var result = DtoMapper.ConvertInvoiceToInvoiceResponseDto(invoice);
        
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.InvoiceNumber, Is.EqualTo(invoice.InvoiceNumber));
            Assert.That(result.Items.Count(), Is.EqualTo(invoice.InvoiceItems.Count));
        });
    }

    [Test]
    public void ConvertIssueStatusHistoryToStatusHistoryResponseDto_ValidStatusHistory_ReturnsExpectedDto()
    {

        var statusHistory = new IssueStatusHistory
        {
            Status = IssueStatus.Completed,
            ChangedAt = DateTime.Now
        };
        
        var result = DtoMapper.ConvertIssueStatusHistoryToStatusHistoryResponseDto(statusHistory);
        
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.Status, Is.EqualTo(statusHistory.Status));
            Assert.That(result.ChangedAt, Is.EqualTo(statusHistory.ChangedAt));
        });
    }
}
