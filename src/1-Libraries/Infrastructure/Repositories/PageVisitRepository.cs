// Copyright (c) CodeBlock.Dev. All rights reserved.
// For more information visit https://codeblock.dev

using CodeBlock.DevKit.Core.Extensions;
using CodeBlock.DevKit.Infrastructure.Database;
using HeyItIsMe.Core.Domain.Reports;
using HeyItIsMe.Infrastructure.DbContext;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HeyItIsMe.Infrastructure.Repositories;

internal class PageVisitRepository : MongoDbBaseRepository<PageVisit>, IPageVisitRepository
{
    private readonly IMongoCollection<PageVisit> _pageVisits;

    public PageVisitRepository(MainDbContext dbContext)
        : base(dbContext.PageVisits)
    {
        _pageVisits = dbContext.PageVisits;
    }

    public async Task<bool> ShouldTrackPageVisit(string pageId, string visitorIp, int thresholdInMinute = 5)
    {
        if (pageId.IsNullOrEmptyOrWhiteSpace() || visitorIp.IsNullOrEmptyOrWhiteSpace())
            return false;

        var thresholdTime = DateTime.UtcNow.AddMinutes(-thresholdInMinute);

        var filter = Builders<PageVisit>.Filter.And(
            Builders<PageVisit>.Filter.Eq(pv => pv.PageId, pageId),
            Builders<PageVisit>.Filter.Eq(pv => pv.IP, visitorIp),
            Builders<PageVisit>.Filter.Gte(pv => pv.CreationTime.DateTime, thresholdTime)
        );

        var recentVisit = await _pageVisits.Find(filter).FirstOrDefaultAsync();
        return recentVisit == null;
    }

    public async Task<Dictionary<DateTime, long>> GetVisitsByDay(string pageId, DateTime fromDateTime, DateTime toDateTime)
    {
        var fromUtc = fromDateTime.ToUniversalTime();
        var toUtc = toDateTime.ToUniversalTime();

        var filter = Builders<PageVisit>.Filter.And(
            Builders<PageVisit>.Filter.Eq(pv => pv.PageId, pageId),
            Builders<PageVisit>.Filter.Gte(pv => pv.CreationTime.DateTime, fromUtc),
            Builders<PageVisit>.Filter.Lte(pv => pv.CreationTime.DateTime, toUtc)
        );

        var result = await _pageVisits
            .Aggregate()
            .Match(filter)
            .Group(
                pv => new
                {
                    pv.CreationTime.Year,
                    pv.CreationTime.Month,
                    pv.CreationTime.Day,
                },
                g => new { Date = new DateTime(g.Key.Year, g.Key.Month, g.Key.Day), Count = g.LongCount() }
            )
            .SortBy(g => g.Date)
            .ToListAsync();

        return result.ToDictionary(r => r.Date, r => r.Count);
    }

    public async Task<Dictionary<DateTime, long>> GetAuthorizedVisitsByDay(string pageId, DateTime fromDateTime, DateTime toDateTime)
    {
        var fromUtc = fromDateTime.ToUniversalTime();
        var toUtc = toDateTime.ToUniversalTime();

        var filter = Builders<PageVisit>.Filter.And(
            Builders<PageVisit>.Filter.Eq(pv => pv.PageId, pageId),
            Builders<PageVisit>.Filter.Ne(pv => pv.VisitorId, ""),
            Builders<PageVisit>.Filter.Gte(pv => pv.CreationTime.DateTime, fromUtc),
            Builders<PageVisit>.Filter.Lte(pv => pv.CreationTime.DateTime, toUtc)
        );

        var result = await _pageVisits
            .Aggregate()
            .Match(filter)
            .Group(
                pv => new
                {
                    pv.CreationTime.Year,
                    pv.CreationTime.Month,
                    pv.CreationTime.Day,
                },
                g => new { Date = new DateTime(g.Key.Year, g.Key.Month, g.Key.Day), Count = g.LongCount() }
            )
            .SortBy(g => g.Date)
            .ToListAsync();

        return result.ToDictionary(r => r.Date, r => r.Count);
    }

    public async Task<Dictionary<DateTime, long>> GetUnauthorizedVisitsByDay(string pageId, DateTime fromDateTime, DateTime toDateTime)
    {
        var fromUtc = fromDateTime.ToUniversalTime();
        var toUtc = toDateTime.ToUniversalTime();

        var filter = Builders<PageVisit>.Filter.And(
            Builders<PageVisit>.Filter.Eq(pv => pv.PageId, pageId),
            Builders<PageVisit>.Filter.Or(
                Builders<PageVisit>.Filter.Eq(pv => pv.VisitorId, ""),
                Builders<PageVisit>.Filter.Eq(pv => pv.VisitorId, null)
            ),
            Builders<PageVisit>.Filter.Gte(pv => pv.CreationTime.DateTime, fromUtc),
            Builders<PageVisit>.Filter.Lte(pv => pv.CreationTime.DateTime, toUtc)
        );

        var result = await _pageVisits
            .Aggregate()
            .Match(filter)
            .Group(
                pv => new
                {
                    pv.CreationTime.Year,
                    pv.CreationTime.Month,
                    pv.CreationTime.Day,
                },
                g => new { Date = new DateTime(g.Key.Year, g.Key.Month, g.Key.Day), Count = g.LongCount() }
            )
            .SortBy(g => g.Date)
            .ToListAsync();

        return result.ToDictionary(r => r.Date, r => r.Count);
    }

    public async Task<Dictionary<DateTime, long>> GetUniqueVisitorsByDay(string pageId, DateTime fromDateTime, DateTime toDateTime)
    {
        var fromUtc = fromDateTime.ToUniversalTime();
        var toUtc = toDateTime.ToUniversalTime();

        var pipeline = new[]
        {
            new BsonDocument(
                "$match",
                new BsonDocument
                {
                    { nameof(PageVisit.PageId), pageId },
                    {
                        $"{nameof(PageVisit.CreationTime)}.DateTime",
                        new BsonDocument { { "$gte", fromUtc }, { "$lte", toUtc } }
                    },
                }
            ),
            new BsonDocument(
                "$group",
                new BsonDocument
                {
                    {
                        "_id",
                        new BsonDocument
                        {
                            { "year", new BsonDocument("$year", $"${nameof(PageVisit.CreationTime)}.DateTime") },
                            { "month", new BsonDocument("$month", $"${nameof(PageVisit.CreationTime)}.DateTime") },
                            { "day", new BsonDocument("$dayOfMonth", $"${nameof(PageVisit.CreationTime)}.DateTime") },
                            { "visitorId", $"${nameof(PageVisit.VisitorId)}" },
                            { "ip", $"${nameof(PageVisit.IP)}" },
                        }
                    },
                }
            ),
            new BsonDocument(
                "$group",
                new BsonDocument
                {
                    {
                        "_id",
                        new BsonDocument
                        {
                            { "year", "$_id.year" },
                            { "month", "$_id.month" },
                            { "day", "$_id.day" },
                        }
                    },
                    { "count", new BsonDocument("$sum", 1) },
                }
            ),
            new BsonDocument("$sort", new BsonDocument("_id", 1)),
        };

        var result = await _pageVisits.AggregateAsync<BsonDocument>(pipeline);
        var docs = await result.ToListAsync();

        var resultDict = new Dictionary<DateTime, long>();
        foreach (var doc in docs)
        {
            var id = doc["_id"].AsBsonDocument;
            var year = id["year"].AsInt32;
            var month = id["month"].AsInt32;
            var day = id["day"].AsInt32;
            var count = doc["count"].ToInt64();

            var date = new DateTime(year, month, day);
            resultDict[date] = count;
        }

        return resultDict;
    }

    public async Task<long> CountTotalVisits(string pageId)
    {
        var filter = Builders<PageVisit>.Filter.Eq(pv => pv.PageId, pageId);
        return await _pageVisits.CountDocumentsAsync(filter);
    }

    public async Task<long> CountAuthorizedVisits(string pageId)
    {
        var filter = Builders<PageVisit>.Filter.And(
            Builders<PageVisit>.Filter.Eq(pv => pv.PageId, pageId),
            Builders<PageVisit>.Filter.Ne(pv => pv.VisitorId, "")
        );
        return await _pageVisits.CountDocumentsAsync(filter);
    }

    public async Task<long> CountUnauthorizedVisits(string pageId)
    {
        var filter = Builders<PageVisit>.Filter.And(
            Builders<PageVisit>.Filter.Eq(pv => pv.PageId, pageId),
            Builders<PageVisit>.Filter.Or(
                Builders<PageVisit>.Filter.Eq(pv => pv.VisitorId, ""),
                Builders<PageVisit>.Filter.Eq(pv => pv.VisitorId, null)
            )
        );
        return await _pageVisits.CountDocumentsAsync(filter);
    }

    public async Task<long> CountUniqueVisitors(string pageId)
    {
        var pipeline = new[]
        {
            new BsonDocument("$match", new BsonDocument(nameof(PageVisit.PageId), pageId)),
            new BsonDocument(
                "$group",
                new BsonDocument
                {
                    {
                        "_id",
                        new BsonDocument { { "visitorId", $"${nameof(PageVisit.VisitorId)}" }, { "ip", $"${nameof(PageVisit.IP)}" } }
                    },
                }
            ),
            new BsonDocument("$count", "uniqueVisitors"),
        };

        var result = await _pageVisits.AggregateAsync<BsonDocument>(pipeline);
        var doc = await result.FirstOrDefaultAsync();

        if (doc == null || !doc.Contains("uniqueVisitors"))
            return 0;

        return doc["uniqueVisitors"].ToInt64();
    }

    public async Task<PageVisit> GetLatestAuthorizedVisits(string pageId, int takeCount = 10)
    {
        var filter = Builders<PageVisit>.Filter.And(
            Builders<PageVisit>.Filter.Eq(pv => pv.PageId, pageId),
            Builders<PageVisit>.Filter.Ne(pv => pv.VisitorId, "")
        );

        var sortDefinition = Builders<PageVisit>.Sort.Descending(pv => pv.CreationTime.DateTime);

        return await _pageVisits.Find(filter).Sort(sortDefinition).FirstOrDefaultAsync();
    }
}
