using System.ComponentModel.DataAnnotations;
using CodeBlock.DevKit.Application.Commands;
using CodeBlock.DevKit.Core.Resources;
using HeyItIsMe.Core.Resources;

namespace HeyItIsMe.Application.UseCases.Facts.AddFact;

internal class AddFactRequest : BaseCommand
{
    public AddFactRequest(string pageId, string content, string title)
    {
        PageId = pageId;
        Content = content;
        Title = title;
    }

    /// <summary>
    /// The unique identifier of the page to add the fact to.
    /// </summary>
    public string PageId { get; }

    /// <summary>
    /// The title of the fact to add. Required field that cannot be empty.
    /// </summary>
    [Display(Name = nameof(SharedResource.Fact_Title), ResourceType = typeof(SharedResource))]
    [Required(ErrorMessageResourceName = nameof(CoreResource.Required), ErrorMessageResourceType = typeof(CoreResource))]
    public string Title { get; }

    /// <summary>
    /// The content of the fact to add. Required field that cannot be empty.
    /// </summary>
    [Display(Name = nameof(SharedResource.Fact_Content), ResourceType = typeof(SharedResource))]
    [Required(ErrorMessageResourceName = nameof(CoreResource.Required), ErrorMessageResourceType = typeof(CoreResource))]
    public string Content { get; }
}
