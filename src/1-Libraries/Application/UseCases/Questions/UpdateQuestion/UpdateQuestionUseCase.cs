using CodeBlock.DevKit.Application.Commands;
using CodeBlock.DevKit.Application.Srvices;
using CodeBlock.DevKit.Core.Helpers;
using HeyItIsMe.Application.Exceptions;
using HeyItIsMe.Core.Domain.Questions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HeyItIsMe.Application.UseCases.Questions.UpdateQuestion;

internal class UpdateQuestionUseCase : BaseCommandHandler, IRequestHandler<UpdateQuestionRequest, CommandResult>
{
    private readonly IQuestionRepository _questionRepository;

    public UpdateQuestionUseCase(IQuestionRepository questionRepository, IRequestDispatcher requestDispatcher, ILogger<UpdateQuestionUseCase> logger)
        : base(requestDispatcher, logger)
    {
        _questionRepository = questionRepository;
    }

    public async Task<CommandResult> Handle(UpdateQuestionRequest request, CancellationToken cancellationToken)
    {
        var question = await _questionRepository.GetByIdAsync(request.Id);
        if (question == null)
            throw QuestionApplicationExceptions.QuestionNotFound(request.Id);

        var loadedVersion = question.Version;

        question.Update(request.Content, request.Description, request.Order);

        await _questionRepository.ConcurrencySafeUpdateAsync(question, loadedVersion);

        await PublishDomainEventsAsync(question.GetDomainEvents());

        return CommandResult.Create(entityId: question.Id);
    }
}
