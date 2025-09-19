using CodeBlock.DevKit.Application.Srvices;
using CodeBlock.DevKit.Contracts.Dtos;
using CodeBlock.DevKit.Core.Helpers;
using HeyItIsMe.Application.Dtos.Pages;
using HeyItIsMe.Application.UseCases.Pages.CreatePage;
using HeyItIsMe.Application.UseCases.Pages.GetPage;
using HeyItIsMe.Application.UseCases.Pages.SearchPages;
using HeyItIsMe.Application.UseCases.Pages.UpdatePage;

namespace HeyItIsMe.Application.Services.Pages;

internal class PageService : ApplicationService, IPageService
{
    public PageService(IRequestDispatcher requestDispatcher)
        : base(requestDispatcher) { }

    public async Task<Result<GetPageDto>> GetPage(string id)
    {
        return await _requestDispatcher.SendQuery(new GetPageRequest(id));
    }

    public async Task<Result<CommandResult>> CreatePage(CreatePageDto input)
    {
        return await _requestDispatcher.SendCommand(new CreatePageRequest(input.Route, input.DisplayName, input.UserId, input.SubscriptionId));
    }

    public async Task<Result<CommandResult>> UpdatePage(string id, UpdatePageDto input)
    {
        return await _requestDispatcher.SendCommand(new UpdatePageRequest(id, input.Route, input.DisplayName));
    }

    public async Task<Result<SearchOutputDto<GetPageDto>>> SearchPages(SearchPagesInputDto input)
    {
        return await _requestDispatcher.SendQuery(
            new SearchPagesRequest(input.Term, input.PageNumber, input.RecordsPerPage, input.SortOrder, input.FromDateTime, input.ToDateTime)
        );
    }
}
