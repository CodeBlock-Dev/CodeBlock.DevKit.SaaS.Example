using AutoMapper;
using CodeBlock.DevKit.Application.Queries;
using CodeBlock.DevKit.Contracts.Dtos;
using HeyItIsMe.Application.Dtos.Questions;
using HeyItIsMe.Core.Domain.Questions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HeyItIsMe.Application.UseCases.Questions.SearchQuestions;

internal class SearchQuestionsUseCase : BaseQueryHandler, IRequestHandler<SearchQuestionsRequest, SearchOutputDto<GetQuestionDto>>
{
    private readonly IQuestionRepository _questionRepository;

    public SearchQuestionsUseCase(IQuestionRepository questionRepository, IMapper mapper, ILogger<SearchQuestionsUseCase> logger)
        : base(mapper, logger)
    {
        _questionRepository = questionRepository;
    }

    public async Task<SearchOutputDto<GetQuestionDto>> Handle(SearchQuestionsRequest request, CancellationToken cancellationToken)
    {
        var questions = await _questionRepository.SearchAsync(request.Term, request.PageNumber, request.RecordsPerPage, request.SortOrder);

        var totalRecords = await _questionRepository.CountAsync(request.Term);

        var questionsDto = _mapper.Map<IEnumerable<GetQuestionDto>>(questions);

        return new SearchOutputDto<GetQuestionDto> { TotalRecords = totalRecords, Items = questionsDto };
    }
}
