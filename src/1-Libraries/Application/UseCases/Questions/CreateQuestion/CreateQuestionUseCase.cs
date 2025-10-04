using CodeBlock.DevKit.Application.Commands;
using CodeBlock.DevKit.Application.Srvices;
using CodeBlock.DevKit.Core.Helpers;
using HeyItIsMe.Core.Domain.Questions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HeyItIsMe.Application.UseCases.Questions.CreateQuestion;

internal class CreateQuestionUseCase : BaseCommandHandler, IRequestHandler<CreateQuestionRequest, CommandResult>
{
    private readonly IQuestionRepository _questionRepository;

    public CreateQuestionUseCase(IQuestionRepository questionRepository, IRequestDispatcher requestDispatcher, ILogger<CreateQuestionUseCase> logger)
        : base(requestDispatcher, logger)
    {
        _questionRepository = questionRepository;
    }

    public async Task<CommandResult> Handle(CreateQuestionRequest request, CancellationToken cancellationToken)
    {
        var question = Question.Create(request.Content, request.Description, request.Order);

        await _questionRepository.AddAsync(question);

        await PublishDomainEventsAsync(question.GetDomainEvents());

        return CommandResult.Create(entityId: question.Id);
    }
}
