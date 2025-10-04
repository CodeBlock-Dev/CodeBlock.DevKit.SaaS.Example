using CodeBlock.DevKit.Application.Queries;
using CodeBlock.DevKit.Core.Helpers;
using HeyItIsMe.Application.Dtos.Questions;

namespace HeyItIsMe.Application.UseCases.Questions.GetQuestion;

internal class GetQuestionRequest : BaseQuery<GetQuestionDto>
{
    public GetQuestionRequest(string id, QueryOptions options = null)
        : base(options)
    {
        Id = id;
    }

    public string Id { get; set; }
}
