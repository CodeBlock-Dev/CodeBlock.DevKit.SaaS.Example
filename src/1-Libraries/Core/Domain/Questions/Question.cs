using CodeBlock.DevKit.Core.Extensions;
using CodeBlock.DevKit.Domain.Entities;

namespace HeyItIsMe.Core.Domain.Questions;

public sealed class Question : AggregateRoot
{
    private Question(string content, string description, int order)
    {
        Content = content;
        Description = description;
        Order = order;

        CheckPolicies();

        AddDomainEvent(new QuestionCreated(Id, Content, Order));
        TrackChange(nameof(QuestionCreated));
    }

    public string Content { get; private set; }
    public string Description { get; private set; }
    public int Order { get; private set; }

    public static Question Create(string content, string description, int order)
    {
        return new Question(content, description, order);
    }

    public void Update(string content, string description, int order)
    {
        if (Content == content && Description == description && Order == order)
            return;

        Content = content;
        Description = description;
        Order = order;

        CheckPolicies();

        AddDomainEvent(new QuestionUpdated(Id, Content, Order));
        TrackChange(nameof(QuestionUpdated));
    }

    protected override void CheckInvariants() { }

    private void CheckPolicies()
    {
        if (Content.IsNullOrEmptyOrWhiteSpace())
            throw QuestionDomainExceptions.ContentIsRequired();

        if (Description.IsNullOrEmptyOrWhiteSpace())
            throw QuestionDomainExceptions.DescriptionIsRequired();
    }
}
