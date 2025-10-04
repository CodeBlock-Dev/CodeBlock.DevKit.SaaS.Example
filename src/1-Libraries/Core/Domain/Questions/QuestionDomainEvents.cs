using CodeBlock.DevKit.Domain.Events;

namespace HeyItIsMe.Core.Domain.Questions;

public record QuestionCreated(string Id, string Content, int Order) : IDomainEvent;

public record QuestionUpdated(string Id, string Content, int Order) : IDomainEvent;
