using System.ComponentModel.DataAnnotations;
using CodeBlock.DevKit.Core.Resources;
using HeyItIsMe.Core.Resources;

namespace HeyItIsMe.Application.Dtos.Questions;

public class CreateQuestionDto
{
    [Display(Name = nameof(SharedResource.Question_Content), ResourceType = typeof(SharedResource))]
    [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.Question_Content))]
    public string Content { get; set; }

    [Display(Name = nameof(SharedResource.Question_Description), ResourceType = typeof(SharedResource))]
    [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.Question_Description))]
    public string Description { get; set; }

    [Display(Name = nameof(SharedResource.Question_Order), ResourceType = typeof(SharedResource))]
    [Required(ErrorMessageResourceName = nameof(CoreResource.Required), ErrorMessageResourceType = typeof(CoreResource))]
    public int Order { get; set; }
}
