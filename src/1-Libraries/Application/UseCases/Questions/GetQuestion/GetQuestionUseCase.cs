using AutoMapper;
using CodeBlock.DevKit.Application.Queries;
using CodeBlock.DevKit.Application.Srvices;
using HeyItIsMe.Application.Dtos.Questions;
using HeyItIsMe.Application.Exceptions;
using HeyItIsMe.Core.Domain.Questions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HeyItIsMe.Application.UseCases.Questions.GetQuestion;

internal class GetQuestionUseCase : BaseQueryHandler, IRequestHandler<GetQuestionRequest, GetQuestionDto>
{
    private readonly IQuestionRepository _questionRepository;

    public GetQuestionUseCase(
        IQuestionRepository questionRepository,
        IMapper mapper,
        ILogger<GetQuestionUseCase> logger
    )
        : base(mapper, logger)
    {
        _questionRepository = questionRepository;
    }

    public async Task<GetQuestionDto> Handle(GetQuestionRequest request, CancellationToken cancellationToken)
    {
        var question = await _questionRepository.GetByIdAsync(request.Id);
        if (question == null)
            throw QuestionApplicationExceptions.QuestionNotFound(request.Id);

        var questionDto = _mapper.Map<GetQuestionDto>(question);

        return questionDto;
    }
}
