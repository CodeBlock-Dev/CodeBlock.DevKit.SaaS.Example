using CodeBlock.DevKit.Core.Extensions;
using CodeBlock.DevKit.Domain.Entities;

namespace HeyItIsMe.Core.Domain.Pages;

/// <summary>
/// Page is a domain entity that represents a user-created page with contacts and facts.
/// This class demonstrates how to implement a complete domain model with aggregate root pattern,
/// including child entities and comprehensive business logic.
///
/// Key features demonstrated:
/// - Domain-driven design with aggregate root pattern
/// - Business rule validation through policies
/// - Domain event publishing for all significant changes
/// - Immutable properties with controlled modification
/// - Factory method pattern for creation
/// - Child entity management (Contacts and Facts)
/// - Collection management with business rules
/// - Repository validation for business rules requiring data access
/// </summary>
public sealed class Page : AggregateRoot
{
    /// <summary>
    /// Private constructor ensures Page can only be created through the Create factory method
    /// and enforces business rules during instantiation.
    /// </summary>
    /// <param name="route">The unique route for the page</param>
    /// <param name="displayName">The display name of the page</param>
    /// <param name="userId">Identifier of the user who owns this page</param>
    /// <param name="subscriptionId">Identifier of the subscription associated with this page</param>
    private Page(string route, string displayName, string userId, string subscriptionId, IPageRepository pageRepository)
    {
        Route = route;
        DisplayName = displayName;
        UserId = userId;
        SubscriptionId = subscriptionId;
        Contacts = new List<Contact>();
        Facts = new List<Fact>();

        CheckPolicies(pageRepository);

        AddDomainEvent(new PageCreated(Id, Route, DisplayName));
        TrackChange(nameof(PageCreated));
    }

    /// <summary>
    /// The unique route for accessing this page. Required field that cannot be empty.
    /// </summary>
    public string Route { get; private set; }

    /// <summary>
    /// The display name shown to users. Required field that cannot be empty.
    /// </summary>
    public string DisplayName { get; private set; }

    /// <summary>
    /// Identifier of the user who owns this page. Required field for ownership tracking.
    /// </summary>
    public string UserId { get; private set; }

    /// <summary>
    /// Identifier of the subscription associated with this page. Required field for billing tracking.
    /// </summary>
    public string SubscriptionId { get; private set; }

    /// <summary>
    /// Collection of contacts associated with this page. Managed through domain methods.
    /// </summary>
    public ICollection<Contact> Contacts { get; private set; }

    /// <summary>
    /// Collection of facts associated with this page. Managed through domain methods.
    /// </summary>
    public ICollection<Fact> Facts { get; private set; }

    /// <summary>
    /// Factory method to create a new Page instance. This method enforces business rules
    /// and ensures proper initialization of the domain entity.
    ///
    /// Example usage:
    /// var page = Page.Create("/my-page", "My Page", "user123", "sub-456");
    /// </summary>
    /// <param name="route">The unique route for the page</param>
    /// <param name="displayName">The display name of the page</param>
    /// <param name="userId">Identifier of the user who owns this page</param>
    /// <param name="subscriptionId">Identifier of the subscription associated with this page</param>
    /// <returns>A new Page instance with validated business rules</returns>
    public static Page Create(string route, string displayName, string userId, string subscriptionId, IPageRepository pageRepository)
    {
        return new Page(route, displayName, userId, subscriptionId, pageRepository);
    }

    /// <summary>
    /// Updates both route and display name of the page. Only modifies the entity if changes are detected,
    /// ensuring efficient domain event publishing and change tracking.
    ///
    /// Example usage:
    /// await page.UpdateAsync("/new-route", "New Display Name", pageRepository);
    /// </summary>
    /// <param name="route">New route for the page</param>
    /// <param name="displayName">New display name for the page</param>
    /// <param name="pageRepository">Repository for checking route uniqueness</param>
    public void Update(string route, string displayName, IPageRepository pageRepository)
    {
        if (Route == route && DisplayName == displayName)
            return;

        Route = route;
        DisplayName = displayName;

        CheckPolicies(pageRepository);

        AddDomainEvent(new PageUpdated(Id, Route, DisplayName));
        TrackChange(nameof(PageUpdated));
    }

    /// <summary>
    /// Adds a new contact to the page. Validates that the contact doesn't already exist
    /// and enforces business rules.
    ///
    /// Example usage:
    /// page.AddContact("Contact information");
    /// </summary>
    /// <param name="content">Content of the contact to add</param>
    /// <returns>The created Contact entity</returns>
    public Contact AddContact(string content)
    {
        var contact = Contact.Create(content);

        // Check if contact with same content already exists
        if (Contacts.Any(c => c.Content == content))
            throw PageDomainExceptions.ContactAlreadyExists();

        Contacts.Add(contact);

        AddDomainEvent(new ContactAdded(Id, contact.Id, content));
        TrackChange(nameof(ContactAdded));

        return contact;
    }

    /// <summary>
    /// Updates an existing contact on the page. Validates that the contact exists
    /// and enforces business rules.
    ///
    /// Example usage:
    /// page.UpdateContact(contactId, "Updated contact information");
    /// </summary>
    /// <param name="contactId">ID of the contact to update</param>
    /// <param name="content">New content for the contact</param>
    public void UpdateContact(string contactId, string content)
    {
        var contact = Contacts.FirstOrDefault(c => c.Id == contactId);
        if (contact == null)
            throw PageDomainExceptions.ContactNotFound();

        contact.Update(content);

        AddDomainEvent(new ContactUpdated(Id, contactId, content));
        TrackChange(nameof(ContactUpdated));
    }

    /// <summary>
    /// Removes a contact from the page. Validates that the contact exists
    /// before removal.
    ///
    /// Example usage:
    /// page.RemoveContact(contactId);
    /// </summary>
    /// <param name="contactId">ID of the contact to remove</param>
    public void RemoveContact(string contactId)
    {
        var contact = Contacts.FirstOrDefault(c => c.Id == contactId);
        if (contact == null)
            throw PageDomainExceptions.ContactNotFound();

        Contacts.Remove(contact);

        AddDomainEvent(new ContactRemoved(Id, contactId));
        TrackChange(nameof(ContactRemoved));
    }

    /// <summary>
    /// Adds a new fact to the page. Validates that the fact doesn't already exist
    /// and enforces business rules.
    ///
    /// Example usage:
    /// page.AddFact("Interesting fact information");
    /// </summary>
    /// <param name="content">Content of the fact to add</param>
    /// <returns>The created Fact entity</returns>
    public Fact AddFact(string content)
    {
        var fact = Fact.Create(content);

        // Check if fact with same content already exists
        if (Facts.Any(f => f.Content == content))
            throw PageDomainExceptions.FactAlreadyExists();

        Facts.Add(fact);

        AddDomainEvent(new FactAdded(Id, fact.Id, content));
        TrackChange(nameof(FactAdded));

        return fact;
    }

    /// <summary>
    /// Updates an existing fact on the page. Validates that the fact exists
    /// and enforces business rules.
    ///
    /// Example usage:
    /// page.UpdateFact(factId, "Updated fact information");
    /// </summary>
    /// <param name="factId">ID of the fact to update</param>
    /// <param name="content">New content for the fact</param>
    public void UpdateFact(string factId, string content)
    {
        var fact = Facts.FirstOrDefault(f => f.Id == factId);
        if (fact == null)
            throw PageDomainExceptions.FactNotFound();

        fact.Update(content);

        AddDomainEvent(new FactUpdated(Id, factId, content));
        TrackChange(nameof(FactUpdated));
    }

    /// <summary>
    /// Removes a fact from the page. Validates that the fact exists
    /// before removal.
    ///
    /// Example usage:
    /// page.RemoveFact(factId);
    /// </summary>
    /// <param name="factId">ID of the fact to remove</param>
    public void RemoveFact(string factId)
    {
        var fact = Facts.FirstOrDefault(f => f.Id == factId);
        if (fact == null)
            throw PageDomainExceptions.FactNotFound();

        Facts.Remove(fact);

        AddDomainEvent(new FactRemoved(Id, factId));
        TrackChange(nameof(FactRemoved));
    }

    /// <summary>
    /// Override point for domain invariant validation. Currently empty as this example
    /// focuses on basic business rule validation through policies.
    /// </summary>
    protected override void CheckInvariants() { }

    /// <summary>
    /// Synchronous version of CheckPolicies for use in constructor where async is not available.
    /// This version only checks basic field validation without route uniqueness.
    /// Route uniqueness should be checked separately after entity creation.
    /// </summary>
    private void CheckPolicies(IPageRepository pageRepository)
    {
        if (Route.IsNullOrEmptyOrWhiteSpace())
            throw PageDomainExceptions.RouteIsRequired();

        if (DisplayName.IsNullOrEmptyOrWhiteSpace())
            throw PageDomainExceptions.DisplayNameIsRequired();

        if (UserId.IsNullOrEmptyOrWhiteSpace())
            throw PageDomainExceptions.UserIdIsRequired();

        if (SubscriptionId.IsNullOrEmptyOrWhiteSpace())
            throw PageDomainExceptions.SubscriptionIdIsRequired();

        var isRouteInUse = pageRepository.IsRouteInUse(Route, Id);
        if (isRouteInUse)
            throw PageDomainExceptions.RouteAlreadyExists();
    }
}
