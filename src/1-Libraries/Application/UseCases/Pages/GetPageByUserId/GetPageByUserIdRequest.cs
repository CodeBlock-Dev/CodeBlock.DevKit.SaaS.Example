using CodeBlock.DevKit.Application.Queries;
using CodeBlock.DevKit.Core.Helpers;
using HeyItIsMe.Application.Dtos.Pages;

namespace HeyItIsMe.Application.UseCases.Pages.GetPageByUserId;

internal class GetPageByUserIdRequest : BaseQuery<GetPageDto>
{
    public GetPageByUserIdRequest(string id, QueryOptions options = null)
        : base(options)
    {
        Id = id;
    }

    /// <summary>
    /// The unique identifier of the page to retrieve.
    /// </summary>
    public string Id { get; set; }
}
