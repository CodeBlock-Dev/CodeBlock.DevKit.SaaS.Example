using CodeBlock.DevKit.Contracts.Dtos;
using CodeBlock.DevKit.Core.Helpers;
using CodeBlock.DevKit.Web.Api.Filters;
using HeyItIsMe.Application.Dtos.Pages;
using HeyItIsMe.Application.Services.Pages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HeyItIsMe.Api.Controllers;

[Tags("Pages")]
[Route("pages")]
[AllowAnonymous]
public class PagesController : BaseApiController
{
    private readonly IPageService _pageService;

    public PagesController(IPageService pageService)
    {
        _pageService = pageService;
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<Result<GetPageDto>> Get(string id)
    {
        return await _pageService.GetPage(id);
    }

    [HttpPost]
    [Authorize]
    public async Task<Result<CommandResult>> Post([FromBody] CreatePageDto input)
    {
        return await _pageService.CreatePage(input);
    }

    [Route("{id}")]
    [HttpPut]
    [Authorize]
    public async Task<Result<CommandResult>> Put(string id, [FromBody] UpdatePageDto input)
    {
        return await _pageService.UpdatePage(id, input);
    }

    [HttpGet]
    [Route("{pageNumber}/{recordsPerPage}/{sortOrder}")]
    public async Task<Result<SearchOutputDto<GetPageDto>>> Get(
        int pageNumber,
        int recordsPerPage,
        SortOrder sortOrder,
        [FromQuery] string term = null,
        [FromQuery] DateTime? fromDateTime = null,
        [FromQuery] DateTime? toDateTime = null
    )
    {
        var dto = new SearchPagesInputDto
        {
            Term = term,
            PageNumber = pageNumber,
            RecordsPerPage = recordsPerPage,
            FromDateTime = fromDateTime,
            ToDateTime = toDateTime,
            SortOrder = sortOrder,
        };
        return await _pageService.SearchPages(dto);
    }
}
