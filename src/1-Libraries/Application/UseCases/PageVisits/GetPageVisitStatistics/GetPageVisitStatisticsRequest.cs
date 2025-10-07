using CodeBlock.DevKit.Application.Queries;
using CodeBlock.DevKit.Core.Helpers;
using HeyItIsMe.Application.Dtos.PageVisits;

namespace HeyItIsMe.Application.UseCases.PageVisits.GetPageVisitStatistics;

internal class GetPageVisitStatisticsRequest : BaseQuery<PageVisitStatisticsDto>
{
    public GetPageVisitStatisticsRequest(string pageId, QueryOptions options = null)
        : base(options)
    {
        PageId = pageId;
    }

    public string PageId { get; set; }
}
