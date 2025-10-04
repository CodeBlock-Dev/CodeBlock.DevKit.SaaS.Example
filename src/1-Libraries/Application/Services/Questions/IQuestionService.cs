using CodeBlock.DevKit.Contracts.Dtos;
using CodeBlock.DevKit.Core.Helpers;
using HeyItIsMe.Application.Dtos.Questions;

namespace HeyItIsMe.Application.Services.Questions;

public interface IQuestionService
{
    Task<Result<GetQuestionDto>> GetQuestion(string id);

    Task<Result<CommandResult>> CreateQuestion(CreateQuestionDto input);

    Task<Result<CommandResult>> UpdateQuestion(string id, UpdateQuestionDto input);

    Task<Result<SearchOutputDto<GetQuestionDto>>> SearchQuestions(SearchQuestionsInputDto input);

    Task<Result<IEnumerable<GetQuestionDto>>> GetOrderedQuestions();
}
