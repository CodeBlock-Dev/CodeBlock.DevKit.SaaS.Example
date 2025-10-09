using CodeBlock.DevKit.Application.Queries;
using CodeBlock.DevKit.Core.Helpers;
using HeyItIsMe.Application.Dtos.Pages;

namespace HeyItIsMe.Application.UseCases.Pages.GetPageByUserId;

internal class GetPageByUserIdRequest : BaseQuery<GetPageDto>
{
    public GetPageByUserIdRequest(string userId, QueryOptions options = null)
        : base(options)
    {
        UserId = userId;
    }

    public string UserId { get; set; }
}
