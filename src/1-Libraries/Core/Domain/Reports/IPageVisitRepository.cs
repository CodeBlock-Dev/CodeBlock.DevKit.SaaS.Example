using CodeBlock.DevKit.Domain.Services;

namespace HeyItIsMe.Core.Domain.Reports;

public interface IPageVisitRepository : IBaseRepository<PageVisit>
{
    Task<bool> ShouldTrackPageVisit(string pageId, string visitorIp, int thresholdInMinute = 5);
    Task<Dictionary<DateTime, long>> GetVisitsByDay(string pageId, DateTime fromDateTime, DateTime toDateTime);
    Task<Dictionary<DateTime, long>> GetAuthorizedVisitsByDay(string pageId, DateTime fromDateTime, DateTime toDateTime);
    Task<Dictionary<DateTime, long>> GetUnauthorizedVisitsByDay(string pageId, DateTime fromDateTime, DateTime toDateTime);
    Task<Dictionary<DateTime, long>> GetUniqueVisitorsByDay(string pageId, DateTime fromDateTime, DateTime toDateTime);
    Task<long> CountTotalVisits(string pageId);
    Task<long> CountAuthorizedVisits(string pageId);
    Task<long> CountUnauthorizedVisits(string pageId);
    Task<long> CountUniqueVisitors(string pageId);
    Task<PageVisit> GetLatestAuthorizedVisits(string pageId, int takeCount = 10);
}
