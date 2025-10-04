// Copyright (c) CodeBlock.Dev. All rights reserved.
// For more information visit https://codeblock.dev

using CodeBlock.DevKit.Core.Extensions;
using CodeBlock.DevKit.Core.Helpers;
using CodeBlock.DevKit.Infrastructure.Database;
using HeyItIsMe.Core.Domain.Questions;
using HeyItIsMe.Infrastructure.DbContext;
using MongoDB.Driver;

namespace HeyItIsMe.Infrastructure.Repositories;

internal class QuestionRepository : MongoDbBaseAggregateRepository<Question>, IQuestionRepository
{
    private readonly IMongoCollection<Question> _questions;

    public QuestionRepository(MainDbContext dbContext)
        : base(dbContext.Questions)
    {
        _questions = dbContext.Questions;
    }

    public async Task<long> CountAsync(string term)
    {
        var filter = GetFilter(term);
        return await _questions.CountDocumentsAsync(filter);
    }

    public async Task<IEnumerable<Question>> SearchAsync(string term, int questionNumber, int recordsPerQuestion, SortOrder sortOrder)
    {
        var filter = GetFilter(term);

        var sortDefinition =
            sortOrder == SortOrder.Desc
                ? Builders<Question>.Sort.Descending(u => u.CreationTime.DateTime)
                : Builders<Question>.Sort.Ascending(u => u.CreationTime.DateTime);

        return await _questions
            .Find(filter)
            .Sort(sortDefinition)
            .Skip(recordsPerQuestion * (questionNumber - 1))
            .Limit(recordsPerQuestion)
            .ToListAsync();
    }

    private FilterDefinition<Question> GetFilter(string term)
    {
        var filterBuilder = Builders<Question>.Filter;
        var filters = new List<FilterDefinition<Question>>();

        if (!term.IsNullOrEmptyOrWhiteSpace())
        {
            filters.Add(
                filterBuilder.Or(
                    filterBuilder.Regex(p => p.Content, new MongoDB.Bson.BsonRegularExpression(term, "i")),
                    filterBuilder.Regex(p => p.Description, new MongoDB.Bson.BsonRegularExpression(term, "i"))
                )
            );
        }

        return filters.Count > 0 ? filterBuilder.And(filters) : filterBuilder.Empty;
    }
}
