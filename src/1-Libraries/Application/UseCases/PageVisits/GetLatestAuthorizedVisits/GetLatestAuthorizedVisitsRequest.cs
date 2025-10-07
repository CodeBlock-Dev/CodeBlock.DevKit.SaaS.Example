using CodeBlock.DevKit.Application.Queries;
using CodeBlock.DevKit.Core.Helpers;
using HeyItIsMe.Application.Dtos.PageVisits;

namespace HeyItIsMe.Application.UseCases.PageVisits.GetLatestAuthorizedVisits;

internal class GetLatestAuthorizedVisitsRequest : BaseQuery<IEnumerable<GetPageVisitDto>>
{
    public GetLatestAuthorizedVisitsRequest(string pageId, int takeCount = 10, QueryOptions options = null)
        : base(options)
    {
        PageId = pageId;
        TakeCount = takeCount;
    }

    public string PageId { get; set; }
    public int TakeCount { get; set; }
}
