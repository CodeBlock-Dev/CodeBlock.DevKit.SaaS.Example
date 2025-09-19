using System.ComponentModel.DataAnnotations;
using CodeBlock.DevKit.Application.Commands;
using CodeBlock.DevKit.Core.Resources;
using HeyItIsMe.Core.Resources;

namespace HeyItIsMe.Application.UseCases.Pages.UpdatePage;

internal class UpdatePageRequest : BaseCommand
{
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
