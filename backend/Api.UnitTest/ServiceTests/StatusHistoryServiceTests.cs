using Api.Data.Entities;
using Api.Models.Enums;
using Api.Repositories.StatusHistoryRepository;
using Api.Services.StatusHistoryService;
using Moq;

namespace Api.UnitTest.ServiceTests;

[TestFixture]
public class StatusHistoryServiceTests
{
    private Mock<IStatusHistoryRepository> _repositoryMock;
    private StatusHistoryService _statusHistoryService;

    [SetUp]
    public void Setup()
    {
        _repositoryMock = new Mock<IStatusHistoryRepository>();
        _statusHistoryService = new StatusHistoryService(_repositoryMock.Object);
    }

    private IssueStatusHistory CreateStatusHistory(Guid issueId, IssueStatus status, DateTime changedAt)
    {
        return new IssueStatusHistory
        {
            IssueId = issueId,
            Status = status,
            ChangedAt = changedAt
        };
    }

    [Test]
    public async Task GetResolveAnalyticsAsync_WithResolvedAndUnresolvedIssues_ReturnsCorrectAnalytics()
    {
        var issueId1 = Guid.NewGuid();
        var issueId2 = Guid.NewGuid();
        var history = new List<IssueStatusHistory>
        {
            CreateStatusHistory(issueId1, IssueStatus.Received, DateTime.UtcNow.AddDays(-5)),
            CreateStatusHistory(issueId1, IssueStatus.Completed, DateTime.UtcNow),
            CreateStatusHistory(issueId2, IssueStatus.Received, DateTime.UtcNow.AddDays(-3))
        };

        _repositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(history);

        var result = await _statusHistoryService.GetResolveAnalyticsAsync();

        Assert.Multiple(() =>
        {
            Assert.That(result.ResolvedCount, Is.EqualTo(1));
            Assert.That(result.UnresolvedCount, Is.EqualTo(1));
            Assert.That(result.AverageResolveTime, Is.EqualTo(5).Within(0.0001));
            Assert.That(result.ResolvePercentage, Is.EqualTo(50));
        });
    }

    [Test]
    public async Task GetResolveAnalyticsAsync_NoHistory_ReturnsZeroAnalytics()
    {
        _repositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<IssueStatusHistory>());

        var result = await _statusHistoryService.GetResolveAnalyticsAsync();

        Assert.That(result.ResolvedCount, Is.EqualTo(0));
        Assert.That(result.UnresolvedCount, Is.EqualTo(0));
        Assert.That(result.AverageResolveTime, Is.EqualTo(0));
        Assert.That(result.ResolvePercentage, Is.EqualTo(0));
    }

    [Test]
    public async Task GetByIssueIdAsync_WithExistingHistory_ReturnsMappedDtos()
    {
        var issueId = Guid.NewGuid();
        var history = new List<IssueStatusHistory>
        {
            CreateStatusHistory(issueId, IssueStatus.Received, DateTime.UtcNow.AddDays(-5)),
            CreateStatusHistory(issueId, IssueStatus.Completed, DateTime.UtcNow)
        };

        _repositoryMock.Setup(r => r.GetByIdAsync(issueId)).ReturnsAsync(history);

        var result = await _statusHistoryService.GetByIssueIdAsync(issueId);

        Assert.That(result.Count(), Is.EqualTo(2));
        Assert.That(result.First().Status, Is.EqualTo(IssueStatus.Received));
        Assert.That(result.Last().Status, Is.EqualTo(IssueStatus.Completed));
    }

    [Test]
    public async Task GetByIssueIdAsync_NoHistory_ReturnsEmptyCollection()
    {
        var issueId = Guid.NewGuid();

        _repositoryMock.Setup(r => r.GetByIdAsync(issueId)).ReturnsAsync(new List<IssueStatusHistory>());

        var result = await _statusHistoryService.GetByIssueIdAsync(issueId);

        Assert.That(result, Is.Empty);
    }
}