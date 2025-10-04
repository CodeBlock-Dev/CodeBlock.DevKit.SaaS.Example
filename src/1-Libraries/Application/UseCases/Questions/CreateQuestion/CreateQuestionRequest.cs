using System.ComponentModel.DataAnnotations;
using CodeBlock.DevKit.Application.Commands;
using CodeBlock.DevKit.Core.Resources;
using HeyItIsMe.Core.Resources;

namespace HeyItIsMe.Application.UseCases.Questions.CreateQuestion;

internal class CreateQuestionRequest : BaseCommand
{
    public CreateQuestionRequest(string content, string description, int order)
    {
        Content = content;
        Description = description;
        Order = order;
    }

    [Display(Name = nameof(SharedResource.Question_Content), ResourceType = typeof(SharedResource))]
    [Required(ErrorMessageResourceName = nameof(CoreResource.Required), ErrorMessageResourceType = typeof(CoreResource))]
    public string Content { get; }

    [Display(Name = nameof(SharedResource.Question_Description), ResourceType = typeof(SharedResource))]
    [Required(ErrorMessageResourceName = nameof(CoreResource.Required), ErrorMessageResourceType = typeof(CoreResource))]
    public string Description { get; }

    [Display(Name = nameof(SharedResource.Question_Order), ResourceType = typeof(SharedResource))]
    [Required(ErrorMessageResourceName = nameof(CoreResource.Required), ErrorMessageResourceType = typeof(CoreResource))]
    public int Order { get; }
}
