using CodeBlock.DevKit.Core.Extensions;
using CodeBlock.DevKit.Domain.Entities;

namespace HeyItIsMe.Core.Domain.Pages;

/// <summary>
/// Fact is a domain entity that represents factual information associated with a Page.
/// This class demonstrates how to implement a child entity within an aggregate root,
/// following the same patterns as the parent entity but with simpler business logic.
/// 
/// Key features demonstrated:
/// - Child entity pattern within an aggregate root
/// - Business rule validation through policies
/// - Immutable properties with controlled modification
/// - Factory method pattern for creation
/// - Update method with change detection
/// </summary>
public sealed class Fact : Entity
{
    /// <summary>
    /// Private constructor ensures Fact can only be created through the Create factory method
    /// and enforces business rules during instantiation.
    /// </summary>
    /// <param name="content">The factual information content</param>
    private Fact(string content)
    {
        Content = content;

        CheckPolicies();
    }

    /// <summary>
    /// The factual information content. Required field that cannot be empty.
    /// </summary>
    public string Content { get; private set; }

    /// <summary>
    /// Factory method to create a new Fact instance. This method enforces business rules
    /// and ensures proper initialization of the domain entity.
    /// 
    /// Example usage:
    /// var fact = Fact.Create("The Earth is approximately 4.5 billion years old.");
    /// </summary>
    /// <param name="content">The factual information content</param>
    /// <returns>A new Fact instance with validated business rules</returns>
    public static Fact Create(string content)
    {
        return new Fact(content);
    }

    /// <summary>
    /// Updates the content of the fact. Only modifies the entity if changes are detected,
    /// ensuring efficient change tracking.
    /// 
    /// Example usage:
    /// fact.Update("Updated factual information.");
    /// </summary>
    /// <param name="content">New content for the fact</param>
    public void Update(string content)
    {
        if (Content == content)
            return;

        Content = content;

        CheckPolicies();
    }

    /// <summary>
    /// Validates business rules and policies for the Fact entity. Throws domain exceptions
    /// if any required fields are missing or invalid.
    /// 
    /// Business rules enforced:
    /// - Content must not be null, empty, or whitespace
    /// </summary>
    private void CheckPolicies()
    {
        if (Content.IsNullOrEmptyOrWhiteSpace())
            throw PageDomainExceptions.FactContentIsRequired();
    }
}
