using System.ComponentModel.DataAnnotations;
using HeyItIsMe.Core.Resources;
using CodeBlock.DevKit.Application.Commands;
using CodeBlock.DevKit.Core.Resources;

namespace HeyItIsMe.Application.UseCases.Facts.AddFact;

/// <summary>
/// Command request for adding a new Fact to a Page.
/// This class demonstrates how to implement command requests with validation attributes,
/// immutable properties, and proper resource-based localization.
/// </summary>
internal class AddFactRequest : BaseCommand
{
    /// <summary>
    /// Initializes a new instance of the AddFactRequest with the required data.
    /// </summary>
    /// <param name="pageId">The unique identifier of the page to add the fact to</param>
    /// <param name="content">The content of the fact to add</param>
    public AddFactRequest(string pageId, string content)
    {
        PageId = pageId;
        Content = content;
    }

    /// <summary>
    /// The unique identifier of the page to add the fact to.
    /// </summary>
    public string PageId { get; }

    /// <summary>
    /// The content of the fact to add. Required field that cannot be empty.
    /// </summary>
    [Display(Name = nameof(SharedResource.Fact_Content), ResourceType = typeof(SharedResource))]
    [Required(ErrorMessageResourceName = nameof(CoreResource.Required), ErrorMessageResourceType = typeof(CoreResource))]
    public string Content { get; }
}
