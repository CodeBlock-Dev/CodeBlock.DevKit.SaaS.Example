using CodeBlock.DevKit.Core.Helpers;
using CodeBlock.DevKit.Web.Api.Filters;
using HeyItIsMe.Application.Dtos.Facts;
using HeyItIsMe.Application.Services.Facts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HeyItIsMe.Api.Controllers;

[Tags("Facts")]
[Route("facts")]
[Authorize]
public class FactsController : BaseApiController
{
    private readonly IFactService _factService;

    public FactsController(IFactService factService)
    {
        _factService = factService;
    }

    [HttpPost]
    public async Task<Result<CommandResult>> Post([FromBody] AddFactDto input)
    {
        return await _factService.AddFact(input);
    }

    [HttpPut]
    public async Task<Result<CommandResult>> Put([FromBody] UpdateFactDto input)
    {
        return await _factService.UpdateFact(input);
    }

    [HttpDelete]
    public async Task<Result<CommandResult>> Delete([FromQuery] string pageId, [FromQuery] string factId)
    {
        return await _factService.RemoveFact(pageId, factId);
    }
}
