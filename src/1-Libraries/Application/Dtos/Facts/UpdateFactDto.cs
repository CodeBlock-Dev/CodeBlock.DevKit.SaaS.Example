using System.ComponentModel.DataAnnotations;
using CodeBlock.DevKit.Core.Resources;
using HeyItIsMe.Core.Resources;

namespace HeyItIsMe.Application.Dtos.Facts;

public class UpdateFactDto
{
    /// <summary>
    /// The unique identifier of the page containing the fact.
    /// </summary>
    [Required(ErrorMessageResourceName = nameof(CoreResource.Required), ErrorMessageResourceType = typeof(CoreResource))]
    public string PageId { get; set; }

    /// <summary>
    /// The unique identifier of the fact to update.
    /// </summary>
    [Required(ErrorMessageResourceName = nameof(CoreResource.Required), ErrorMessageResourceType = typeof(CoreResource))]
    public string FactId { get; set; }

    /// <summary>
    /// The new content for the fact. Required field that cannot be empty.
    /// </summary>
    [Display(Name = nameof(SharedResource.Fact_Content), ResourceType = typeof(SharedResource))]
    [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.Fact_Content))]
    public string Content { get; set; }
}
