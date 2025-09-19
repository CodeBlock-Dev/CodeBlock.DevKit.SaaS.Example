using CodeBlock.DevKit.Application.Extensions;
using HeyItIsMe.Application.Services.Contacts;
using HeyItIsMe.Application.Services.DemoThings;
using HeyItIsMe.Application.Services.Facts;
using HeyItIsMe.Application.Services.Pages;
using Microsoft.Extensions.DependencyInjection;

namespace HeyItIsMe.Application;

/// <summary>
/// Startup configuration class for the Application module.
/// This class demonstrates how to configure dependency injection for application services.
/// The DemoThing functionality shown here is just an example to help you learn how to implement
/// your own unique features into the current codebase.
/// </summary>
public static class Startup
{
    /// <summary>
    /// Registers all application services and handlers with the dependency injection container.
    /// This method is called during application startup to configure the Application module.
    /// </summary>
    /// <param name="services">The service collection to register services with</param>
    public static void AddApplicationModule(this IServiceCollection services)
    {
        services.RegisterHandlers(typeof(Startup));

        services.AddScoped<IDemoThingService, DemoThingService>();
        services.AddScoped<IPageService, PageService>();
        services.AddScoped<IFactService, FactService>();
        services.AddScoped<IContactService, ContactService>();
    }
}
