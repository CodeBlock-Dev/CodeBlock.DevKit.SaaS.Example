using System.ComponentModel.DataAnnotations;
using CodeBlock.DevKit.Application.Commands;
using CodeBlock.DevKit.Core.Resources;
using HeyItIsMe.Core.Resources;

namespace HeyItIsMe.Application.UseCases.Pages.UpdatePageDisplayName;

internal class UpdatePageDisplayNameRequest : BaseCommand
{
    public UpdatePageDisplayNameRequest(string id, string displayName)
    {
        Id = id;
        DisplayName = displayName;
    }

    /// <summary>
    /// The unique identifier of the page to update.
    /// </summary>
    public string Id { get; }

    /// <summary>
    /// The new display name shown to users. Required field that cannot be empty.
    /// </summary>
    [Display(Name = nameof(SharedResource.Page_DisplayName), ResourceType = typeof(SharedResource))]
    [Required(ErrorMessageResourceName = nameof(CoreResource.Required), ErrorMessageResourceType = typeof(CoreResource))]
    public string DisplayName { get; }
}
