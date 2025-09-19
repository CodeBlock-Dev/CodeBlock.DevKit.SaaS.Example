using CodeBlock.DevKit.Application.Srvices;
using CodeBlock.DevKit.Core.Helpers;
using HeyItIsMe.Application.Dtos.Facts;
using HeyItIsMe.Application.UseCases.Facts.AddFact;
using HeyItIsMe.Application.UseCases.Facts.RemoveFact;
using HeyItIsMe.Application.UseCases.Facts.UpdateFact;

namespace HeyItIsMe.Application.Services.Facts;

internal class FactService : ApplicationService, IFactService
{
    public FactService(IRequestDispatcher requestDispatcher)
        : base(requestDispatcher) { }

    public async Task<Result<CommandResult>> AddFact(AddFactDto input)
    {
        return await _requestDispatcher.SendCommand(new AddFactRequest(input.PageId, input.Content));
    }

    public async Task<Result<CommandResult>> UpdateFact(UpdateFactDto input)
    {
        return await _requestDispatcher.SendCommand(new UpdateFactRequest(input.PageId, input.FactId, input.Content));
    }

    public async Task<Result<CommandResult>> RemoveFact(string pageId, string factId)
    {
        return await _requestDispatcher.SendCommand(new RemoveFactRequest(pageId, factId));
    }
}
