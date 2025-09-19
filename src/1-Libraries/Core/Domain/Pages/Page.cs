using CodeBlock.DevKit.Core.Extensions;
using CodeBlock.DevKit.Domain.Entities;

namespace HeyItIsMe.Core.Domain.Pages;

public sealed class Page : AggregateRoot
{
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

    public static Page Create(string route, string displayName, string userId, string subscriptionId, IPageRepository pageRepository)
    {
        return new Page(route, displayName, userId, subscriptionId, pageRepository);
    }

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

    public void UpdateContact(string contactId, string content)
    {
        var contact = Contacts.FirstOrDefault(c => c.Id == contactId);
        if (contact == null)
            throw PageDomainExceptions.ContactNotFound();

        contact.Update(content);

        AddDomainEvent(new ContactUpdated(Id, contactId, content));
        TrackChange(nameof(ContactUpdated));
    }

    public void RemoveContact(string contactId)
    {
        var contact = Contacts.FirstOrDefault(c => c.Id == contactId);
        if (contact == null)
            throw PageDomainExceptions.ContactNotFound();

        Contacts.Remove(contact);

        AddDomainEvent(new ContactRemoved(Id, contactId));
        TrackChange(nameof(ContactRemoved));
    }

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

    public void UpdateFact(string factId, string content)
    {
        var fact = Facts.FirstOrDefault(f => f.Id == factId);
        if (fact == null)
            throw PageDomainExceptions.FactNotFound();

        fact.Update(content);

        AddDomainEvent(new FactUpdated(Id, factId, content));
        TrackChange(nameof(FactUpdated));
    }

    public void RemoveFact(string factId)
    {
        var fact = Facts.FirstOrDefault(f => f.Id == factId);
        if (fact == null)
            throw PageDomainExceptions.FactNotFound();

        Facts.Remove(fact);

        AddDomainEvent(new FactRemoved(Id, factId));
        TrackChange(nameof(FactRemoved));
    }

    protected override void CheckInvariants() { }

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
