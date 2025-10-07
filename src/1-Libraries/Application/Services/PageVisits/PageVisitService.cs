using CodeBlock.DevKit.Application.Srvices;
using CodeBlock.DevKit.Core.Helpers;
using HeyItIsMe.Application.Dtos.PageVisits;
using HeyItIsMe.Application.UseCases.PageVisits.GetLatestAuthorizedVisits;
using HeyItIsMe.Application.UseCases.PageVisits.GetPageVisitStatistics;
using HeyItIsMe.Application.UseCases.PageVisits.GetVisitsByDay;

namespace HeyItIsMe.Application.Services.PageVisits;

internal class PageVisitService : ApplicationService, IPageVisitService
{
    public PageVisitService(IRequestDispatcher requestDispatcher)
        : base(requestDispatcher) { }

    public async Task<Result<IEnumerable<GetPageVisitstByDayDto>>> GetVisitsByDay(string pageId, DateTime fromDateTime, DateTime toDateTime)
    {
        return await _requestDispatcher.SendQuery(new GetVisitsByDayRequest(pageId, fromDateTime, toDateTime));
    }

    public async Task<Result<PageVisitStatisticsDto>> GetPageVisitStatistics(string pageId)
    {
        return await _requestDispatcher.SendQuery(new GetPageVisitStatisticsRequest(pageId));
    }

    public async Task<Result<IEnumerable<GetPageVisitDto>>> GetLatestAuthorizedVisits(string pageId, int takeCount = 10)
    {
        return await _requestDispatcher.SendQuery(new GetLatestAuthorizedVisitsRequest(pageId, takeCount));
    }
}
