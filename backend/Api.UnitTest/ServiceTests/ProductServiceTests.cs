using Api.Data.Entities;
using Api.Exceptions;
using Api.Repositories.ProductRepository;
using Api.Services.ProductService;
using Moq;

namespace Api.UnitTest.ServiceTests;

[TestFixture]
public class ProductServiceTests
{
    private Mock<IProductRepository> _repositoryMock;
    private ProductService _productService;

    [SetUp]
    public void Setup()
    {
        _repositoryMock = new Mock<IProductRepository>();
        _productService = new ProductService(_repositoryMock.Object);
    }

    private Product GetSampleProduct(Guid id)
    {
        return new Product
        {
            Id = id,
            Name = "Test Product",
            Sku = "TP123"
        };
    }

    [Test]
    public async Task GetByProductIdAsync_ProductFound_ReturnsProductResponseDto()
    {
        var productId = Guid.NewGuid();
        var product = GetSampleProduct(productId);

        _repositoryMock
            .Setup(r => r.GetByProductIdAsync(productId))
            .ReturnsAsync(product);

        var result = await _productService.GetByProductIdAsync(productId);

        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.Id, Is.EqualTo(productId));
            Assert.That(result.Name, Is.EqualTo(product.Name));
        });
        _repositoryMock.Verify(r => r.GetByProductIdAsync(productId), Times.Once);
    }

    [Test]
    public void GetByProductIdAsync_ProductNotFound_ThrowsNotFoundException()
    {
        var productId = Guid.NewGuid();

        _repositoryMock
            .Setup(r => r.GetByProductIdAsync(productId))
            .ReturnsAsync((Product)null);

        var exception = Assert.ThrowsAsync<NotFoundException>(() => _productService.GetByProductIdAsync(productId));
        Assert.That(exception.Message, Is.EqualTo($"Product with ID {productId} not found."));
        _repositoryMock.Verify(r => r.GetByProductIdAsync(productId), Times.Once);
    }

    [Test]
    public async Task GetAllAsync_ProductsFound_ReturnsProductResponseDtos()
    {
        var products = new List<Product>
        {
            GetSampleProduct(Guid.NewGuid()),
            GetSampleProduct(Guid.NewGuid())
        };

        _repositoryMock
            .Setup(r => r.GetAllAsync())
            .ReturnsAsync(products);

        var result = await _productService.GetAllAsync();

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count(), Is.EqualTo(products.Count));
        _repositoryMock.Verify(r => r.GetAllAsync(), Times.Once);
    }

    [Test]
    public async Task GetAllAsync_NoProductsFound_ReturnsEmptyCollection()
    {
        _repositoryMock
            .Setup(r => r.GetAllAsync())
            .ReturnsAsync(new List<Product>());

        var result = await _productService.GetAllAsync();

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.Empty);
        _repositoryMock.Verify(r => r.GetAllAsync(), Times.Once);
    }
}