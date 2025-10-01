using CodeBlock.DevKit.Domain.Events;

namespace HeyItIsMe.Core.Domain.Pages;

/// <summary>
/// Event raised when a new Page entity is created. This event contains the essential
/// information about the newly created page that other parts of the system might need.
/// </summary>
/// <param name="Id">Unique identifier of the created Page</param>
/// <param name="Route">Route of the created Page</param>
/// <param name="DisplayName">Display name of the created Page</param>
public record PageCreated(string Id, string Route, string DisplayName) : IDomainEvent;

/// <summary>
/// Event raised when an existing Page entity is updated. This event contains the essential
/// information about the updated page that other parts of the system might need.
/// </summary>
/// <param name="Id">Unique identifier of the updated Page</param>
/// <param name="Route">Updated route of the Page</param>
/// <param name="DisplayName">Updated display name of the Page</param>
public record PageUpdated(string Id, string Route, string DisplayName) : IDomainEvent;

/// <summary>
/// Event raised when a Contact is added to a Page. This event contains the essential
/// information about the newly added contact that other parts of the system might need.
/// </summary>
/// <param name="PageId">Unique identifier of the Page that the contact was added to</param>
/// <param name="ContactId">Unique identifier of the added Contact</param>
/// <param name="Content">Content of the added Contact</param>
public record ContactAdded(string PageId, string ContactId, string Content) : IDomainEvent;

/// <summary>
/// Event raised when a Contact is updated. This event contains the essential
/// information about the updated contact that other parts of the system might need.
/// </summary>
/// <param name="PageId">Unique identifier of the Page that contains the updated contact</param>
/// <param name="ContactId">Unique identifier of the updated Contact</param>
/// <param name="Content">Updated content of the Contact</param>
public record ContactUpdated(string PageId, string ContactId, string Content) : IDomainEvent;

/// <summary>
/// Event raised when a Contact is removed from a Page. This event contains the essential
/// information about the removed contact that other parts of the system might need.
/// </summary>
/// <param name="PageId">Unique identifier of the Page that the contact was removed from</param>
/// <param name="ContactId">Unique identifier of the removed Contact</param>
public record ContactRemoved(string PageId, string ContactId) : IDomainEvent;

/// <summary>
/// Event raised when a Fact is added to a Page. This event contains the essential
/// information about the newly added fact that other parts of the system might need.
/// </summary>
/// <param name="PageId">Unique identifier of the Page that the fact was added to</param>
/// <param name="FactId">Unique identifier of the added Fact</param>
/// <param name="Content">Content of the added Fact</param>
public record FactAdded(string PageId, string FactId, string Content) : IDomainEvent;

/// <summary>
/// Event raised when a Fact is updated. This event contains the essential
/// information about the updated fact that other parts of the system might need.
///
/// Example usage in event handlers:
/// </summary>
/// <param name="PageId">Unique identifier of the Page that contains the updated fact</param>
/// <param name="FactId">Unique identifier of the updated Fact</param>
/// <param name="Content">Updated content of the Fact</param>
public record FactUpdated(string PageId, string FactId, string Content) : IDomainEvent;

/// <summary>
/// Event raised when a Fact is removed from a Page. This event contains the essential
/// information about the removed fact that other parts of the system might need.
/// </summary>
/// <param name="PageId">Unique identifier of the Page that the fact was removed from</param>
/// <param name="FactId">Unique identifier of the removed Fact</param>
public record FactRemoved(string PageId, string FactId) : IDomainEvent;
