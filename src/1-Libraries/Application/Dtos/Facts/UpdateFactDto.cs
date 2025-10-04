using System.ComponentModel.DataAnnotations;
using HeyItIsMe.Core.Resources;

namespace HeyItIsMe.Application.Dtos.Facts;

public class UpdateFactDto
{
    /// <summary>
    /// The new title for the fact. Required field that cannot be empty.
    /// </summary>
    [Display(Name = nameof(SharedResource.Fact_Title), ResourceType = typeof(SharedResource))]
    [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.Fact_Content))]
    public string Title { get; set; }

    /// <summary>
    /// The new content for the fact. Required field that cannot be empty.
    /// </summary>
    [Display(Name = nameof(SharedResource.Fact_Content), ResourceType = typeof(SharedResource))]
    [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.Fact_Content))]
    public string Content { get; set; }
}
