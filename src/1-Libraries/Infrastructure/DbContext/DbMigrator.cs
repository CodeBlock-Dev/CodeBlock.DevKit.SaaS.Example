using CodeBlock.DevKit.Infrastructure.Database.Migrations;
using Microsoft.Extensions.DependencyInjection;

namespace HeyItIsMe.Infrastructure.DbContext;

internal static class DbMigrator
{
    public static void MigrateDatabes(this IServiceProvider serviceProvider)
    {
        using var serviceScope = serviceProvider.CreateScope();
        var dbContext = serviceScope.ServiceProvider.GetService<MainDbContext>();
        var dbMigrationRunner = serviceScope.ServiceProvider.GetService<IDbMigrationRunner>();

        var migrations = new List<IDbMigration>
        {
            // Add migrations here as needed
        };

        foreach (var migration in migrations)
        {
            dbMigrationRunner.ApplyMigration(migration);
        }
    }
}
