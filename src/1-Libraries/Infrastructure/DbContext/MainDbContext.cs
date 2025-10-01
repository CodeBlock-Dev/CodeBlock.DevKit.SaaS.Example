using CodeBlock.DevKit.Infrastructure.Database;
using HeyItIsMe.Core.Domain.Pages;
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
    /// <summary>
    /// Initializes a new instance of MainDbContext with MongoDB settings.
    /// </summary>
    /// <param name="mongoDbSettings">MongoDB connection and configuration settings</param>
    public MainDbContext(MongoDbSettings mongoDbSettings)
        : base(mongoDbSettings) { }

    /// <summary>
    /// MongoDB collection for Page entities.
    /// This property provides access to the Pages collection for CRUD operations.
    /// </summary>
    public IMongoCollection<Page> Pages { get; private set; }

    /// <summary>
    /// Creates database indexes for optimal query performance.
    /// This method demonstrates how to set up indexes on commonly queried fields.
    ///
    /// Example: Creates a non-unique index on the Name field for faster text searches.
    /// </summary>
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

    /// <summary>
    /// Safely drops test databases for testing purposes.
    /// Only drops databases that start with "Test_" prefix to prevent accidental
    /// deletion of production databases.
    ///
    /// Example usage in test cleanup:
    /// dbContext.DropTestDatabase();
    /// </summary>
    public void DropTestDatabase()
    {
        // Only drop the database if it starts with "Test_" to avoid dropping production databases.
        if (!_mongoDbSettings.DatabaseName.StartsWith("Test_"))
            return;

        _client.DropDatabase(_mongoDbSettings.DatabaseName);
    }
}
