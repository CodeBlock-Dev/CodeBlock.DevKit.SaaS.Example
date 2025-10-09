// Copyright (c) CodeBlock.Dev. All rights reserved.
// For more information visit https://codeblock.dev

using CodeBlock.DevKit.Core.Extensions;
using CodeBlock.DevKit.Core.Helpers;
using CodeBlock.DevKit.Infrastructure.Database;
using HeyItIsMe.Core.Domain.Pages;
using HeyItIsMe.Infrastructure.DbContext;
using MongoDB.Driver;

namespace HeyItIsMe.Infrastructure.Repositories;

internal class PageRepository : MongoDbBaseAggregateRepository<Page>, IPageRepository
{
    private readonly IMongoCollection<Page> _pages;

    public PageRepository(MainDbContext dbContext)
        : base(dbContext.Pages)
    {
        _pages = dbContext.Pages;
    }

    public async Task<long> CountAsync(string term, DateTime? fromDateTime, DateTime? toDateTime)
    {
        var filter = GetFilter(term, fromDateTime, toDateTime);
        return await _pages.CountDocumentsAsync(filter);
    }

    public async Task<Page> GetByFactIdAsync(string factId)
    {
        return await _pages.Find(p => p.Facts.Any(f => f.Id == factId)).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Page>> SearchAsync(
        string term,
        int pageNumber,
        int recordsPerPage,
        SortOrder sortOrder,
        DateTime? fromDateTime,
        DateTime? toDateTime
    )
    {
        var filter = GetFilter(term, fromDateTime, toDateTime);

        var sortDefinition =
            sortOrder == SortOrder.Desc
                ? Builders<Page>.Sort.Descending(u => u.CreationTime.DateTime)
                : Builders<Page>.Sort.Ascending(u => u.CreationTime.DateTime);

        return await _pages.Find(filter).Sort(sortDefinition).Skip(recordsPerPage * (pageNumber - 1)).Limit(recordsPerPage).ToListAsync();
    }

    public async Task<Page> GetByRouteAsync(string route)
    {
        if (route.IsNullOrEmptyOrWhiteSpace())
            return null;

        var filter = Builders<Page>.Filter.Eq(p => p.Route, route);
        return await _pages.Find(filter).FirstOrDefaultAsync();
    }

    public bool IsRouteInUse(string route, string excludePageId = null)
    {
        if (route.IsNullOrEmptyOrWhiteSpace())
            return false;

        var filterBuilder = Builders<Page>.Filter;
        var filter = filterBuilder.Eq(p => p.Route, route);

        // Exclude the specified page ID if provided (useful for updates)
        if (!excludePageId.IsNullOrEmptyOrWhiteSpace())
        {
            filter = filterBuilder.And(filter, filterBuilder.Ne(p => p.Id, excludePageId));
        }

        return _pages.Find(filter).Any();
    }

    public async Task<Page> GetByUserIdAsync(string userId)
    {
        return await _pages.Find(p => p.UserId == userId).FirstOrDefaultAsync();
    }

    private FilterDefinition<Page> GetFilter(string term, DateTime? fromDateTime, DateTime? toDateTime)
    {
        var filterBuilder = Builders<Page>.Filter;
        var filters = new List<FilterDefinition<Page>>();

        if (fromDateTime.HasValue)
            filters.Add(filterBuilder.Gte(t => t.CreationTime.DateTime, fromDateTime.Value.ToUniversalTime()));

        if (toDateTime.HasValue)
            filters.Add(filterBuilder.Lte(t => t.CreationTime.DateTime, toDateTime.Value.ToUniversalTime()));

        if (!term.IsNullOrEmptyOrWhiteSpace())
        {
            filters.Add(
                filterBuilder.Or(
                    filterBuilder.Regex(p => p.Route, new MongoDB.Bson.BsonRegularExpression(term, "i")),
                    filterBuilder.Regex(p => p.DisplayName, new MongoDB.Bson.BsonRegularExpression(term, "i")),
                    filterBuilder.Regex(p => p.UserId, new MongoDB.Bson.BsonRegularExpression(term, "i"))
                )
            );
        }

        return filters.Count > 0 ? filterBuilder.And(filters) : filterBuilder.Empty;
    }
}
