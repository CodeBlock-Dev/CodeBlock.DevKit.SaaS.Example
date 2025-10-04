using System.ComponentModel.DataAnnotations;
using CodeBlock.DevKit.Application.Commands;
using CodeBlock.DevKit.Core.Resources;
using HeyItIsMe.Core.Resources;

namespace HeyItIsMe.Application.UseCases.Facts.UpdateFact;

internal class UpdateFactRequest : BaseCommand
{
    public UpdateFactRequest(string factId, string title, string content)
    {
        FactId = factId;
        Content = content;
        Title = title;
    }

    /// <summary>
    /// The unique identifier of the fact to update.
    /// </summary>
    public string FactId { get; }

    /// <summary>
    /// The new title for the fact. Required field that cannot be empty.
    /// </summary>
    [Display(Name = nameof(SharedResource.Fact_Title), ResourceType = typeof(SharedResource))]
    [Required(ErrorMessageResourceName = nameof(CoreResource.Required), ErrorMessageResourceType = typeof(CoreResource))]
    public string Title { get; }

    /// <summary>
    /// The new content for the fact. Required field that cannot be empty.
    /// </summary>
    [Display(Name = nameof(SharedResource.Fact_Content), ResourceType = typeof(SharedResource))]
    [Required(ErrorMessageResourceName = nameof(CoreResource.Required), ErrorMessageResourceType = typeof(CoreResource))]
    public string Content { get; }
}
