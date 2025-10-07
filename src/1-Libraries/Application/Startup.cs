using CodeBlock.DevKit.Application.Extensions;
using HeyItIsMe.Application.Services.Contacts;
using HeyItIsMe.Application.Services.Facts;
using HeyItIsMe.Application.Services.Pages;
using HeyItIsMe.Application.Services.PageVisits;
using HeyItIsMe.Application.Services.Questions;
using Microsoft.Extensions.DependencyInjection;

namespace HeyItIsMe.Application;

public static class Startup
{
    public static void AddApplicationModule(this IServiceCollection services)
    {
        services.RegisterHandlers(typeof(Startup));

        services.AddScoped<IPageService, PageService>();
        services.AddScoped<IPageVisitService, PageVisitService>();
        services.AddScoped<IFactService, FactService>();
        services.AddScoped<IContactService, ContactService>();
        services.AddScoped<IQuestionService, QuestionService>();
    }
}
