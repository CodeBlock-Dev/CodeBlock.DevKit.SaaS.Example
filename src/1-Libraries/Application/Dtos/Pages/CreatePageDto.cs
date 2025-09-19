using System.ComponentModel.DataAnnotations;
using CodeBlock.DevKit.Core.Resources;
using HeyItIsMe.Core.Resources;

namespace HeyItIsMe.Application.Dtos.Pages;

public class CreatePageDto
{
    /// <summary>
    /// The unique route for accessing this page. Required field that cannot be empty.
    /// </summary>
    [Display(Name = nameof(SharedResource.Page_Route), ResourceType = typeof(SharedResource))]
    [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.Page_Route))]
    public string Route { get; set; }

    /// <summary>
    /// The display name shown to users. Required field that cannot be empty.
    /// </summary>
    [Display(Name = nameof(SharedResource.Page_DisplayName), ResourceType = typeof(SharedResource))]
    [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.Page_DisplayName))]
    public string DisplayName { get; set; }

    /// <summary>
    /// Identifier of the user who owns this page. Required field for ownership tracking.
    /// </summary>
    [Display(Name = nameof(SharedResource.Page_UserId), ResourceType = typeof(SharedResource))]
    [Required(ErrorMessageResourceName = nameof(CoreResource.Required), ErrorMessageResourceType = typeof(CoreResource))]
    public string UserId { get; set; }

    /// <summary>
    /// Identifier of the subscription associated with this page. Required field for billing tracking.
    /// </summary>
    [Display(Name = nameof(SharedResource.Page_SubscriptionId), ResourceType = typeof(SharedResource))]
    [Required(ErrorMessageResourceName = nameof(CoreResource.Required), ErrorMessageResourceType = typeof(CoreResource))]
    public string SubscriptionId { get; set; }
}
