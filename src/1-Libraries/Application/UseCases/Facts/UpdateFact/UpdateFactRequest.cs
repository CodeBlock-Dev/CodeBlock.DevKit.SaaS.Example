using System.ComponentModel.DataAnnotations;
using CodeBlock.DevKit.Application.Commands;
using CodeBlock.DevKit.Core.Resources;
using HeyItIsMe.Core.Resources;

namespace HeyItIsMe.Application.UseCases.Facts.UpdateFact;

internal class UpdateFactRequest : BaseCommand
{
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
