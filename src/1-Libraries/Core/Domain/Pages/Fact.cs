using CodeBlock.DevKit.Core.Extensions;
using CodeBlock.DevKit.Domain.Entities;

namespace HeyItIsMe.Core.Domain.Pages;

public sealed class Fact : Entity
{
    private Fact(string title, string content)
    {
        Content = content;
        Title = title;

        CheckPolicies();
    }

    public string Title { get; private set; }
    public string Content { get; private set; }

    public static Fact Create(string title, string content)
    {
        return new Fact(title, content);
    }

    public void Update(string title, string content)
    {
        if (Content == content && Title == title)
            return;

        Content = content;
        Title = title;

        CheckPolicies();
    }

    private void CheckPolicies()
    {
        if (Content.IsNullOrEmptyOrWhiteSpace())
            throw PageDomainExceptions.FactContentIsRequired();

        if (Title.IsNullOrEmptyOrWhiteSpace())
            throw PageDomainExceptions.FactTitleIsRequired();
    }
}
