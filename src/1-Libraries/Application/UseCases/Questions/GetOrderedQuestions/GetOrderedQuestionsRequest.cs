using CodeBlock.DevKit.Application.Queries;
using CodeBlock.DevKit.Core.Helpers;
using HeyItIsMe.Application.Dtos.Questions;

namespace HeyItIsMe.Application.UseCases.Questions.GetOrderedQuestions;

internal class GetOrderedQuestionsRequest : BaseQuery<IEnumerable<GetQuestionDto>>
{
    public GetOrderedQuestionsRequest(QueryOptions options = null)
        : base(options)
    {
    }
}
