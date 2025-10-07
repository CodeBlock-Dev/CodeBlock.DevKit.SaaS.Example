using CodeBlock.DevKit.Infrastructure.Database;
using HeyItIsMe.Core.Domain.Pages;
using HeyItIsMe.Core.Domain.Questions;
using HeyItIsMe.Core.Domain.Reports;
using MongoDB.Driver;

namespace HeyItIsMe.Infrastructure.DbContext;

internal class MainDbContext : MongoDbContext
{
    public MainDbContext(MongoDbSettings mongoDbSettings)
        : base(mongoDbSettings) { }

    public IMongoCollection<Page> Pages { get; private set; }
    public IMongoCollection<Question> Questions { get; private set; }
    public IMongoCollection<PageVisit> PageVisits { get; private set; }

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

        PageVisits.Indexes.CreateOne(
            new CreateIndexModel<PageVisit>(
                Builders<PageVisit>.IndexKeys.Ascending(x => x.PageId),
                new CreateIndexOptions() { Name = nameof(PageVisit.PageId), Unique = false }
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
