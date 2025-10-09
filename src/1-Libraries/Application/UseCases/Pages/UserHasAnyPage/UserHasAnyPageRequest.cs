using CodeBlock.DevKit.Application.Queries;
using CodeBlock.DevKit.Core.Helpers;

namespace HeyItIsMe.Application.UseCases.Pages.UserHasAnyPage;

internal class UserHasAnyPageRequest : BaseQuery<bool>
{
    public UserHasAnyPageRequest(string userId, QueryOptions options = null)
        : base(options)
    {
        UserId = userId;
    }

    /// <summary>
    /// The unique identifier of the user to check for page existence.
    /// </summary>
    public string UserId { get; set; }
}
