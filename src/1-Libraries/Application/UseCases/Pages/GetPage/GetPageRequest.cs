using CodeBlock.DevKit.Application.Queries;
using CodeBlock.DevKit.Core.Helpers;
using HeyItIsMe.Application.Dtos.Pages;

namespace HeyItIsMe.Application.UseCases.Pages.GetPage;

/// <summary>
/// Query request for retrieving a Page entity by its identifier.
/// This class demonstrates how to implement query requests that extend base query classes
/// and include optional query options for flexible data retrieval.
/// </summary>
internal class GetPageRequest : BaseQuery<GetPageDto>
{
    /// <summary>
    /// Initializes a new instance of the GetPageRequest with the required identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the page to retrieve</param>
    /// <param name="options">Optional query options for customizing the retrieval behavior</param>
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
