using AutoMapper;
using CodeBlock.DevKit.Application.Queries;
using HeyItIsMe.Application.Dtos.Questions;
using HeyItIsMe.Core.Domain.Questions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HeyItIsMe.Application.UseCases.Questions.GetOrderedQuestions;

internal class GetOrderedQuestionsUseCase : BaseQueryHandler, IRequestHandler<GetOrderedQuestionsRequest, IEnumerable<GetQuestionDto>>
{
    private readonly IQuestionRepository _questionRepository;

    public GetOrderedQuestionsUseCase(IQuestionRepository questionRepository, IMapper mapper, ILogger<GetOrderedQuestionsUseCase> logger)
        : base(mapper, logger)
    {
        _questionRepository = questionRepository;
    }

    public async Task<IEnumerable<GetQuestionDto>> Handle(GetOrderedQuestionsRequest request, CancellationToken cancellationToken)
    {
        var questions = await _questionRepository.GetListAsync();

        var orderedQuestions = questions.OrderBy(q => q.Order);

        var questionsDto = _mapper.Map<IEnumerable<GetQuestionDto>>(orderedQuestions);

        return questionsDto;
    }
}
