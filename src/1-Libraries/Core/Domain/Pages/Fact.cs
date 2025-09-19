using CodeBlock.DevKit.Core.Extensions;
using CodeBlock.DevKit.Domain.Entities;

namespace HeyItIsMe.Core.Domain.Pages;

public sealed class Fact : Entity
{
    private Fact(string content)
    {
        Content = content;

        CheckPolicies();
    }

    public string Content { get; private set; }

    public static Fact Create(string content)
    {
        return new Fact(content);
    }

    public void Update(string content)
    {
        if (Content == content)
            return;

        Content = content;

        CheckPolicies();
    }

    private void CheckPolicies()
    {
        if (Content.IsNullOrEmptyOrWhiteSpace())
            throw PageDomainExceptions.FactContentIsRequired();
    }
}
