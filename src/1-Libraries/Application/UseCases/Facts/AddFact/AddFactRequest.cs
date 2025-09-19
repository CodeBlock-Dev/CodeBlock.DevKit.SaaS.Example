using System.ComponentModel.DataAnnotations;
using CodeBlock.DevKit.Application.Commands;
using CodeBlock.DevKit.Core.Resources;
using HeyItIsMe.Core.Resources;

namespace HeyItIsMe.Application.UseCases.Facts.AddFact;

internal class AddFactRequest : BaseCommand
{
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
