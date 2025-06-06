using Api.Data.Entities;
using Api.Exceptions;
using Api.Models.Enums;
using Api.Models.IssueDtos;
using Api.Repositories.IssueRepository;
using Api.Services.IssueService;
using Moq;

namespace Api.UnitTest.ServiceTests;

[TestFixture]
public class IssueServiceTests
{
    private Mock<IIssueRepository> _repositoryMock;
    private IssueService _service;

    [SetUp]
    public void SetUp()
    {
        _repositoryMock = new Mock<IIssueRepository>();
        _service = new IssueService(_repositoryMock.Object);
    }

    [Test]
    public async Task GetAllAsync_ShouldReturnListOfIssueResponseDto_WhenIssuesExist()
    {
        var issues = CreateIssues();
        _repositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(issues);

        var result = await _service.GetAllAsync();

        Assert.Multiple(() =>
        {
            Assert.That(result.Count(), Is.EqualTo(issues.Count));
            Assert.That(result.First().Title, Is.EqualTo(issues.First().Title));
        });
    }

    [Test]
    public async Task GetByIdAsync_ShouldReturnIssueResponseDto_WhenIssueExists()
    {
        var issue = CreateIssue();
        _repositoryMock.Setup(r => r.GetByIdAsync(issue.Id)).ReturnsAsync(issue);

        var result = await _service.GetByIdAsync(issue.Id);

        Assert.That(result.Id, Is.EqualTo(issue.Id));
    }

    [Test]
    public void GetByIdAsync_ShouldThrowNotFoundException_WhenIssueDoesNotExist()
    {
        var id = Guid.NewGuid();
        _repositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((Issue)null);

        Assert.ThrowsAsync<NotFoundException>(() => _service.GetByIdAsync(id));
    }

    [Test]
    public async Task CreateAsync_ShouldReturnGuid_WhenIssueIsCreated()
    {
        var id = Guid.NewGuid();
        var issueDto = new IssueCreateDto(IssueType.Repair,"New Issue", "This is a test", Guid.Empty, "1id");
        _repositoryMock.Setup(r => r.CreateAsync(It.IsAny<Issue>())).ReturnsAsync(id);

        var result = await _service.CreateAsync(issueDto);

        Assert.That(result, Is.EqualTo(id));
    }

    [Test]
    public async Task UpdateAsync_ShouldUpdateIssue_WhenChangesExist()
    {
        var issue = CreateIssue();
        var updateDto = new IssueUpdateDto(issue.Id, null,"New Title",null, null, null);
        _repositoryMock.Setup(r => r.GetByIdAsync(issue.Id)).ReturnsAsync(issue);

        await _service.UpdateAsync(updateDto);

        _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Issue>()), Times.Once);
    }

    [Test]
    public void UpdateAsync_ShouldThrowNotFoundException_WhenIssueDoesNotExist()
    {
        var updateDto = new IssueUpdateDto (Guid.NewGuid(), null, "New Title", null, null, null);
        _repositoryMock.Setup(r => r.GetByIdAsync(updateDto.IssueId)).ReturnsAsync((Issue)null);

        Assert.ThrowsAsync<NotFoundException>(() => _service.UpdateAsync(updateDto));
    }

    [Test]
    public async Task DeleteAsync_ShouldCallDelete_WhenIssueExists()
    {
        var issue = CreateIssue();
        _repositoryMock.Setup(r => r.GetByIdAsync(issue.Id)).ReturnsAsync(issue);

        await _service.DeleteAsync(issue.Id);

        _repositoryMock.Verify(r => r.DeleteAsync(issue), Times.Once);
    }

    [Test]
    public void DeleteAsync_ShouldThrowNotFoundException_WhenIssueDoesNotExist()
    {
        var id = Guid.NewGuid();
        _repositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((Issue)null);

        Assert.ThrowsAsync<NotFoundException>(() => _service.DeleteAsync(id));
    }

    [Test]
    public async Task GetByFilterAsync_ShouldReturnFilteredIssues_WhenFilterIsNotEmpty()
    {
    
        var filter = new IssueFilterDto (null, null,null,null,null,null,null,"Test");
        var issues = CreateIssues();
        _repositoryMock.Setup(r => r.FilterAsync(filter)).ReturnsAsync(issues);

        var result = await _service.GetByFilterAsync(filter);

        Assert.That(result.Count(), Is.EqualTo(issues.Count));
    }

    [Test]
    public async Task UpdateStatusAsync_ShouldUpdateStatus_WhenIssueExists()
    {
        var issue = CreateIssue();
        var statusUpdateDto = new IssueStatusUpdateDto (IssueStatus.Completed,"Resolved");
        _repositoryMock.Setup(r => r.GetByIdAsync(issue.Id)).ReturnsAsync(issue);

        await _service.UpdateStatusAsync(issue.Id, statusUpdateDto);

        _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Issue>()), Times.Once);
    }

    [Test]
    public void UpdateStatusAsync_ShouldThrowNotFoundException_WhenIssueDoesNotExist()
    {
        var id = Guid.NewGuid();
        var statusUpdateDto = new IssueStatusUpdateDto(IssueStatus.Completed, "Resolved" );
        _repositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((Issue)null);

        Assert.ThrowsAsync<NotFoundException>(() => _service.UpdateStatusAsync(id, statusUpdateDto));
    }

    private List<Issue> CreateIssues()
    {
        var creatorId = Guid.NewGuid().ToString();
        var customerId = Guid.NewGuid();

        return
        [
            new Issue
            {
                Id = Guid.NewGuid(),
                Title = "Issue 1",
                Description = "Desc 1",
                CreatorId = creatorId,
                Creator = new User { UserName = "Creator1" },
                InvoiceId = Guid.NewGuid(),
                Invoice = new Invoice
                {
                    CustomerId = customerId,
                    InvoiceNumber = "INV-001",
                    Customer = new Customer { Name = "Customer 1" }
                },
                Status = IssueStatus.Received,
                Notes = "Note 1",
                CreatedAt = DateTime.UtcNow
            },

            new Issue
            {
                Id = Guid.NewGuid(),
                Title = "Issue 2",
                Description = "Desc 2",
                CreatorId = creatorId,
                Creator = new User { UserName = "Creator2" },
                InvoiceId = Guid.NewGuid(),
                Invoice = new Invoice
                {
                    CustomerId = customerId,
                    InvoiceNumber = "INV-002",
                    Customer = new Customer { Name = "Customer 2" }
                },
                Status = IssueStatus.Received,
                Notes = null,
                CreatedAt = DateTime.UtcNow
            }
        ];
    }

    private Issue CreateIssue()
    {
        var id = Guid.NewGuid();
        var customerId = Guid.NewGuid();

        return new Issue 
        {
            Id = id,
            Title = "Issue",
            Description = "Desc",
            CreatorId = "creator-1",
            Creator = new User { UserName = "Admin" },
            InvoiceId = Guid.NewGuid(),
            Invoice = new Invoice 
            {
                CustomerId = customerId,
                InvoiceNumber = "INV-123",
                Customer = new Customer { Name = "Customer X" }
            },
            Status = IssueStatus.Received,
            Notes = null,
            CreatedAt = DateTime.UtcNow
        };
    }
}