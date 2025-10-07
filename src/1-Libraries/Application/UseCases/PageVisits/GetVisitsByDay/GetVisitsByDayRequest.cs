using CodeBlock.DevKit.Application.Queries;
using CodeBlock.DevKit.Core.Helpers;
using HeyItIsMe.Application.Dtos.PageVisits;

namespace HeyItIsMe.Application.UseCases.PageVisits.GetVisitsByDay;

internal class GetVisitsByDayRequest : BaseQuery<IEnumerable<GetPageVisitstByDayDto>>
{
    public GetVisitsByDayRequest(string pageId, DateTime fromDateTime, DateTime toDateTime, QueryOptions options = null)
        : base(options)
    {
        PageId = pageId;
        FromDateTime = fromDateTime;
        ToDateTime = toDateTime;
    }

    public string PageId { get; set; }
    public DateTime FromDateTime { get; set; }
    public DateTime ToDateTime { get; set; }
}
