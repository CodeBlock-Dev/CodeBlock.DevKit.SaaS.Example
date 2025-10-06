using CodeBlock.DevKit.Application.Extensions;
using CodeBlock.DevKit.Infrastructure.Mapping;
using FluentValidation;
using HeyItIsMe.Application;
using HeyItIsMe.Application.Contracts;
using HeyItIsMe.Core.Domain.Pages;
using HeyItIsMe.Core.Domain.Questions;
using HeyItIsMe.Infrastructure.DbContext;
using HeyItIsMe.Infrastructure.Mapping;
using HeyItIsMe.Infrastructure.Repositories;
using HeyItIsMe.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HeyItIsMe.Infrastructure;

public static class Startup
{
    public static void AddInfrastructureModule(this IServiceCollection services)
    {
        services.AddApplicationModule();
        services.RegisterHandlers(typeof(Startup));
        services.AddMongoDbContext();
        services.AddServices();
        services.AddMappingProfileFromAssemblyContaining<MappingProfile>();
        services.AddValidatorsFromAssembly(typeof(Startup).Assembly);
    }

    public static void UseInfrastructureModule(this IServiceProvider serviceProvider)
    {
        serviceProvider.MigrateDatabes();
        serviceProvider.SeedPermissions();
    }

    public static void DropTestDatabase(this IServiceProvider serviceProvider)
    {
        using var serviceScope = serviceProvider.CreateScope();
        var dbContext = serviceScope.ServiceProvider.GetService<MainDbContext>();
        dbContext.DropTestDatabase();
    }

    private static void AddMongoDbContext(this IServiceCollection services)
    {
        services.AddScoped<MainDbContext>();
    }

    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IPageRepository, PageRepository>();
        services.AddScoped<IQuestionRepository, QuestionRepository>();
        services.AddScoped<IAIImageService, GeminiImageService>();
        services.AddScoped<IAITextService, GeminiTextService>();
        services.AddScoped<IImageService, ImageService>();
    }
}
