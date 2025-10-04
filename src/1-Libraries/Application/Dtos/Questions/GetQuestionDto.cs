using CodeBlock.DevKit.Contracts.Dtos;

namespace HeyItIsMe.Application.Dtos.Questions;

public class GetQuestionDto : GetEntityDto
{
    public string Content { get; set; }

    public string Description { get; set; }

    public int Order { get; set; }
}
