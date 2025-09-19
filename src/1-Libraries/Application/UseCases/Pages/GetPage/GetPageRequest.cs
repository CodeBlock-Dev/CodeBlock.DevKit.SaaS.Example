using CodeBlock.DevKit.Application.Queries;
using CodeBlock.DevKit.Core.Helpers;
using HeyItIsMe.Application.Dtos.Pages;

namespace HeyItIsMe.Application.UseCases.Pages.GetPage;

internal class GetPageRequest : BaseQuery<GetPageDto>
{
    public GetPageRequest(string id, QueryOptions options = null)
        : base(options)
    {
        Id = id;
    }

    /// <summary>
    /// The unique identifier of the page to retrieve.
    /// </summary>
    public string Id { get; set; }
}
