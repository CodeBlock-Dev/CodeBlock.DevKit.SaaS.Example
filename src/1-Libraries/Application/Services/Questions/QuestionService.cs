using CodeBlock.DevKit.Application.Srvices;
using CodeBlock.DevKit.Contracts.Dtos;
using CodeBlock.DevKit.Core.Helpers;
using HeyItIsMe.Application.Dtos.Questions;
using HeyItIsMe.Application.UseCases.Questions.CreateQuestion;
using HeyItIsMe.Application.UseCases.Questions.GetOrderedQuestions;
using HeyItIsMe.Application.UseCases.Questions.GetQuestion;
using HeyItIsMe.Application.UseCases.Questions.SearchQuestions;
using HeyItIsMe.Application.UseCases.Questions.UpdateQuestion;

namespace HeyItIsMe.Application.Services.Questions;

internal class QuestionService : ApplicationService, IQuestionService
{
    public QuestionService(IRequestDispatcher requestDispatcher)
        : base(requestDispatcher) { }

    public async Task<Result<GetQuestionDto>> GetQuestion(string id)
    {
        return await _requestDispatcher.SendQuery(new GetQuestionRequest(id));
    }

    public async Task<Result<CommandResult>> CreateQuestion(CreateQuestionDto input)
    {
        return await _requestDispatcher.SendCommand(new CreateQuestionRequest(input.Content, input.Description, input.Order));
    }

    public async Task<Result<CommandResult>> UpdateQuestion(string id, UpdateQuestionDto input)
    {
        return await _requestDispatcher.SendCommand(new UpdateQuestionRequest(id, input.Content, input.Description, input.Order));
    }

    public async Task<Result<SearchOutputDto<GetQuestionDto>>> SearchQuestions(SearchQuestionsInputDto input)
    {
        return await _requestDispatcher.SendQuery(
            new SearchQuestionsRequest(input.Term, input.PageNumber, input.RecordsPerPage, input.SortOrder, input.FromDateTime, input.ToDateTime)
        );
    }

    public async Task<Result<IEnumerable<GetQuestionDto>>> GetOrderedQuestions()
    {
        return await _requestDispatcher.SendQuery(new GetOrderedQuestionsRequest());
    }
}
