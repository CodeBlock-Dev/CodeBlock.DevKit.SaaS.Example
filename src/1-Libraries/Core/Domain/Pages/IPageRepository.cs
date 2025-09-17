using CodeBlock.DevKit.Core.Helpers;
using CodeBlock.DevKit.Domain.Services;

namespace HeyItIsMe.Core.Domain.Pages;

/// <summary>
/// Repository interface for Page domain entities. This interface demonstrates how to extend
/// the base repository with custom query methods specific to your business requirements.
///
/// This implementation follows the same patterns as IDemoThingRepository, demonstrating repository
/// pattern usage for a complete domain model with aggregate root and child entities.
///
/// Key features demonstrated:
/// - Extending base repository interface for specific entity types
/// - Custom search and counting methods with filtering capabilities
/// - Pagination and sorting support
/// - Date range filtering for temporal queries
/// - Methods for querying child entities (Contacts, Facts)
/// </summary>
public interface IPageRepository : IBaseAggregateRepository<Page>
{
    /// <summary>
    /// Counts the total number of Page entities matching the specified search criteria.
    /// This method is useful for implementing pagination and providing total count information
    /// to clients.
    ///
    /// Example usage:
    /// var totalCount = await repository.CountAsync("search term", "user123",
    ///     DateTime.Today.AddDays(-30), DateTime.Today);
    /// </summary>
    /// <param name="term">Search term to filter by route or display name</param>
    /// <param name="userId">Optional filter by user ID for ownership</param>
    /// <param name="fromDateTime">Optional start date for temporal filtering</param>
    /// <param name="toDateTime">Optional end date for temporal filtering</param>
    /// <returns>Total count of matching entities</returns>
    Task<long> CountAsync(string term, string userId, DateTime fromDateTime, DateTime toDateTime);

    /// <summary>
    /// Searches for Page entities with advanced filtering, pagination, and sorting capabilities.
    /// This method demonstrates how to implement complex query operations that are common in
    /// real-world applications.
    ///
    /// Example usage:
    /// var results = await repository.SearchAsync("search term", "user123",
    ///     1, 20, SortOrder.Ascending, DateTime.Today.AddDays(-30), DateTime.Today);
    /// </summary>
    /// <param name="term">Search term to filter by route or display name</param>
    /// <param name="userId">Optional filter by user ID for ownership</param>
    /// <param name="pageNumber">Page number for pagination (1-based)</param>
    /// <param name="recordsPerPage">Number of records per page</param>
    /// <param name="sortOrder">Sorting order for the results</param>
    /// <param name="fromDateTime">Optional start date for temporal filtering</param>
    /// <param name="toDateTime">Optional end date for temporal filtering</param>
    /// <returns>Collection of Page entities matching the criteria</returns>
    Task<IEnumerable<Page>> SearchAsync(
        string term,
        string userId,
        int pageNumber,
        int recordsPerPage,
        SortOrder sortOrder,
        DateTime fromDateTime,
        DateTime toDateTime
    );

    /// <summary>
    /// Finds a Page entity by its route. This method is useful for ensuring route uniqueness
    /// and for retrieving pages by their public-facing route.
    ///
    /// Example usage:
    /// var page = await repository.GetByRouteAsync("/my-page");
    /// </summary>
    /// <param name="route">The route to search for</param>
    /// <returns>The Page entity with the specified route, or null if not found</returns>
    Task<Page> GetByRouteAsync(string route);

    /// <summary>
    /// Checks if a route is already in use by another page. This method is useful for
    /// validating route uniqueness before creating or updating a page.
    ///
    /// Example usage:
    /// var isRouteInUse = await repository.IsRouteInUseAsync("/my-page", "page-id-to-exclude");
    /// </summary>
    /// <param name="route">The route to check</param>
    /// <param name="excludePageId">Optional page ID to exclude from the check (useful when updating)</param>
    /// <returns>True if the route is in use, false otherwise</returns>
    bool IsRouteInUse(string route, string excludePageId = null);

    /// <summary>
    /// Gets all pages owned by a specific user. This method is useful for displaying
    /// user-specific page listings and managing user content.
    ///
    /// Example usage:
    /// var userPages = await repository.GetByUserIdAsync("user123");
    /// </summary>
    /// <param name="userId">The user ID to filter by</param>
    /// <returns>Collection of Page entities owned by the specified user</returns>
    Task<IEnumerable<Page>> GetByUserIdAsync(string userId);

    /// <summary>
    /// Gets all pages associated with a specific subscription. This method is useful for
    /// subscription-based access control and billing management.
    ///
    /// Example usage:
    /// var subscriptionPages = await repository.GetBySubscriptionIdAsync("sub-123");
    /// </summary>
    /// <param name="subscriptionId">The subscription ID to filter by</param>
    /// <returns>Collection of Page entities associated with the specified subscription</returns>
    Task<IEnumerable<Page>> GetBySubscriptionIdAsync(string subscriptionId);

    /// <summary>
    /// Counts the total number of pages owned by a specific user. This method is useful
    /// for implementing user-specific pagination and quota management.
    ///
    /// Example usage:
    /// var userPageCount = await repository.CountByUserIdAsync("user123");
    /// </summary>
    /// <param name="userId">The user ID to count pages for</param>
    /// <returns>Total count of pages owned by the specified user</returns>
    Task<long> CountByUserIdAsync(string userId);

    /// <summary>
    /// Counts the total number of pages associated with a specific subscription. This method
    /// is useful for subscription quota management and billing calculations.
    ///
    /// Example usage:
    /// var subscriptionPageCount = await repository.CountBySubscriptionIdAsync("sub-123");
    /// </summary>
    /// <param name="subscriptionId">The subscription ID to count pages for</param>
    /// <returns>Total count of pages associated with the specified subscription</returns>
    Task<long> CountBySubscriptionIdAsync(string subscriptionId);
}
