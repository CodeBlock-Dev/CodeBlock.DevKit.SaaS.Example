using System.ComponentModel.DataAnnotations;
using CodeBlock.DevKit.Core.Resources;
using HeyItIsMe.Core.Resources;

namespace HeyItIsMe.Application.Dtos.Facts;

/// <summary>
/// Data Transfer Object for adding a new Fact to a Page.
/// This class demonstrates how to create DTOs with validation attributes and resource-based localization.
/// </summary>
public class AddFactDto
{
    /// <summary>
    /// The unique identifier of the page to add the fact to.
    /// </summary>
    [Required(ErrorMessageResourceName = nameof(CoreResource.Required), ErrorMessageResourceType = typeof(CoreResource))]
    public string PageId { get; set; }

    /// <summary>
    /// The content of the fact to add. Required field that cannot be empty.
    /// </summary>
    [Display(Name = nameof(SharedResource.Fact_Content), ResourceType = typeof(SharedResource))]
    [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.Fact_Content))]
    public string Content { get; set; }
}
