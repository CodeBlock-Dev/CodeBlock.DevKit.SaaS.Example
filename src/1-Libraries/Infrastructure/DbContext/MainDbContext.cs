using CodeBlock.DevKit.Infrastructure.Database;
using HeyItIsMe.Core.Domain.Pages;
using HeyItIsMe.Core.Domain.Questions;
using MongoDB.Driver;

namespace HeyItIsMe.Infrastructure.DbContext;

/// <summary>
/// Main database context for the application that extends MongoDbContext to provide
/// MongoDB-specific functionality. This class demonstrates how to set up a database context
/// with collections, indexes, and custom database operations.
///
/// Key features demonstrated:
/// - MongoDB collection management
/// - Database index creation
/// - Safe test database operations
/// - Collection access through properties
/// </summary>
internal class MainDbContext : MongoDbContext
{
    public MainDbContext(MongoDbSettings mongoDbSettings)
        : base(mongoDbSettings) { }

    public IMongoCollection<Page> Pages { get; private set; }
    public IMongoCollection<Question> Questions { get; private set; }

    protected override void CreateIndexes()
    {
        Pages.Indexes.CreateOne(
            new CreateIndexModel<Page>(
                Builders<Page>.IndexKeys.Ascending(x => x.Route),
                new CreateIndexOptions() { Name = nameof(Page.Route), Unique = true }
            )
        );

        Pages.Indexes.CreateOne(
            new CreateIndexModel<Page>(
                Builders<Page>.IndexKeys.Ascending(x => x.UserId),
                new CreateIndexOptions() { Name = nameof(Page.UserId), Unique = false }
            )
        );
    }

    public void DropTestDatabase()
    {
        // Only drop the database if it starts with "Test_" to avoid dropping production databases.
        if (!_mongoDbSettings.DatabaseName.StartsWith("Test_"))
            return;

        _client.DropDatabase(_mongoDbSettings.DatabaseName);
    }
}
