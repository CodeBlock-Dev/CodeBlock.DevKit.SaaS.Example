using CodeBlock.DevKit.Core.Helpers;
using CodeBlock.DevKit.Domain.Services;

namespace HeyItIsMe.Core.Domain.Pages;

public interface IPageRepository : IBaseAggregateRepository<Page>
{
    Task<long> CountAsync(string term, DateTime? fromDateTime, DateTime? toDateTime);

    Task<IEnumerable<Page>> SearchAsync(
        string term,
        int pageNumber,
        int recordsPerPage,
        SortOrder sortOrder,
        DateTime? fromDateTime,
        DateTime? toDateTime
    );

    Task<Page> GetByRouteAsync(string route);

    bool IsRouteInUse(string route, string excludePageId = null);

    Task<Page> GetByUserIdAsync(string userId);
    Task<Page> GetByFactIdAsync(string factId);
}
