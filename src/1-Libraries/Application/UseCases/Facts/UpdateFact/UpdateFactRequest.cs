using System.ComponentModel.DataAnnotations;
using HeyItIsMe.Core.Resources;
using CodeBlock.DevKit.Application.Commands;
using CodeBlock.DevKit.Core.Resources;

namespace HeyItIsMe.Application.UseCases.Facts.UpdateFact;

/// <summary>
/// Command request for updating an existing Fact on a Page.
/// This class demonstrates how to implement update command requests with validation attributes,
/// immutable properties, and proper resource-based localization.
/// </summary>
internal class UpdateFactRequest : BaseCommand
{
    /// <summary>
    /// Initializes a new instance of the UpdateFactRequest with the required data.
    /// </summary>
    /// <param name="pageId">The unique identifier of the page containing the fact</param>
    /// <param name="factId">The unique identifier of the fact to update</param>
    /// <param name="content">The new content for the fact</param>
    public UpdateFactRequest(string pageId, string factId, string content)
    {
        PageId = pageId;
        FactId = factId;
        Content = content;
    }

    /// <summary>
    /// The unique identifier of the page containing the fact.
    /// </summary>
    public string PageId { get; }

    /// <summary>
    /// The unique identifier of the fact to update.
    /// </summary>
    public string FactId { get; }

    /// <summary>
    /// The new content for the fact. Required field that cannot be empty.
    /// </summary>
    [Display(Name = nameof(SharedResource.Fact_Content), ResourceType = typeof(SharedResource))]
    [Required(ErrorMessageResourceName = nameof(CoreResource.Required), ErrorMessageResourceType = typeof(CoreResource))]
    public string Content { get; }
}
