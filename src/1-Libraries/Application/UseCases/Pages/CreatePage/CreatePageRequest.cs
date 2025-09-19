using System.ComponentModel.DataAnnotations;
using HeyItIsMe.Core.Resources;
using CodeBlock.DevKit.Application.Commands;
using CodeBlock.DevKit.Core.Resources;

namespace HeyItIsMe.Application.UseCases.Pages.CreatePage;

/// <summary>
/// Command request for creating a new Page entity.
/// This class demonstrates how to implement command requests with validation attributes,
/// immutable properties, and proper resource-based localization.
/// </summary>
internal class CreatePageRequest : BaseCommand
{
    /// <summary>
    /// Initializes a new instance of the CreatePageRequest with the required data.
    /// </summary>
    /// <param name="route">The unique route for the page</param>
    /// <param name="displayName">The display name of the page</param>
    /// <param name="userId">Identifier of the user who owns this page</param>
    /// <param name="subscriptionId">Identifier of the subscription associated with this page</param>
    public CreatePageRequest(string route, string displayName, string userId, string subscriptionId)
    {
        Route = route;
        DisplayName = displayName;
        UserId = userId;
        SubscriptionId = subscriptionId;
    }

    /// <summary>
    /// The unique route for accessing this page. Required field that cannot be empty.
    /// </summary>
    [Display(Name = nameof(SharedResource.Page_Route), ResourceType = typeof(SharedResource))]
    [Required(ErrorMessageResourceName = nameof(CoreResource.Required), ErrorMessageResourceType = typeof(CoreResource))]
    public string Route { get; }

    /// <summary>
    /// The display name shown to users. Required field that cannot be empty.
    /// </summary>
    [Display(Name = nameof(SharedResource.Page_DisplayName), ResourceType = typeof(SharedResource))]
    [Required(ErrorMessageResourceName = nameof(CoreResource.Required), ErrorMessageResourceType = typeof(CoreResource))]
    public string DisplayName { get; }

    /// <summary>
    /// Identifier of the user who owns this page. Required field for ownership tracking.
    /// </summary>
    [Display(Name = nameof(SharedResource.Page_UserId), ResourceType = typeof(SharedResource))]
    [Required(ErrorMessageResourceName = nameof(CoreResource.Required), ErrorMessageResourceType = typeof(CoreResource))]
    public string UserId { get; }

    /// <summary>
    /// Identifier of the subscription associated with this page. Required field for billing tracking.
    /// </summary>
    [Display(Name = nameof(SharedResource.Page_SubscriptionId), ResourceType = typeof(SharedResource))]
    [Required(ErrorMessageResourceName = nameof(CoreResource.Required), ErrorMessageResourceType = typeof(CoreResource))]
    public string SubscriptionId { get; }
}
