using System.ComponentModel.DataAnnotations;
using CodeBlock.DevKit.Core.Resources;
using HeyItIsMe.Core.Resources;

namespace HeyItIsMe.Application.Dtos.Pages;

/// <summary>
/// Data Transfer Object for updating an existing Page entity.
/// This class demonstrates how to create update DTOs with validation attributes and resource-based localization.
/// </summary>
public class UpdatePageDto
{
    /// <summary>
    /// The new route for accessing this page. Required field that cannot be empty.
    /// </summary>
    [Display(Name = nameof(SharedResource.Page_Route), ResourceType = typeof(SharedResource))]
    [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.Page_Route))]
    public string Route { get; set; }

    /// <summary>
    /// The new display name shown to users. Required field that cannot be empty.
    /// </summary>
    [Display(Name = nameof(SharedResource.Page_DisplayName), ResourceType = typeof(SharedResource))]
    [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.Page_DisplayName))]
    public string DisplayName { get; set; }
}
