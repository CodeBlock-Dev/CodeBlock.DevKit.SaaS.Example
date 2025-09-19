using System.ComponentModel.DataAnnotations;
using HeyItIsMe.Core.Resources;
using CodeBlock.DevKit.Application.Commands;
using CodeBlock.DevKit.Core.Resources;

namespace HeyItIsMe.Application.UseCases.Pages.UpdatePage;

/// <summary>
/// Command request for updating an existing Page entity.
/// This class demonstrates how to implement update command requests with validation attributes,
/// immutable properties, and proper resource-based localization.
/// </summary>
internal class UpdatePageRequest : BaseCommand
{
    /// <summary>
    /// Initializes a new instance of the UpdatePageRequest with the required data.
    /// </summary>
    /// <param name="id">The unique identifier of the page to update</param>
    /// <param name="route">The new route for the page</param>
    /// <param name="displayName">The new display name for the page</param>
    public UpdatePageRequest(string id, string route, string displayName)
    {
        Id = id;
        Route = route;
        DisplayName = displayName;
    }

    /// <summary>
    /// The unique identifier of the page to update.
    /// </summary>
    public string Id { get; }

    /// <summary>
    /// The new route for accessing this page. Required field that cannot be empty.
    /// </summary>
    [Display(Name = nameof(SharedResource.Page_Route), ResourceType = typeof(SharedResource))]
    [Required(ErrorMessageResourceName = nameof(CoreResource.Required), ErrorMessageResourceType = typeof(CoreResource))]
    public string Route { get; }

    /// <summary>
    /// The new display name shown to users. Required field that cannot be empty.
    /// </summary>
    [Display(Name = nameof(SharedResource.Page_DisplayName), ResourceType = typeof(SharedResource))]
    [Required(ErrorMessageResourceName = nameof(CoreResource.Required), ErrorMessageResourceType = typeof(CoreResource))]
    public string DisplayName { get; }
}
