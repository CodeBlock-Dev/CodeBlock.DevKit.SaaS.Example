using CodeBlock.DevKit.Core.Helpers;
using HeyItIsMe.Application.Dtos.PageVisits;

namespace HeyItIsMe.Application.Services.PageVisits;

public interface IPageVisitService
{
    Task<Result<IEnumerable<GetPageVisitstByDayDto>>> GetVisitsByDay(string pageId, DateTime fromDateTime, DateTime toDateTime);
    Task<Result<PageVisitStatisticsDto>> GetPageVisitStatistics(string pageId);
    Task<Result<IEnumerable<GetPageVisitDto>>> GetLatestAuthorizedVisits(string pageId, int takeCount = 10);
}
