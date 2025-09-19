using CodeBlock.DevKit.Application.Srvices;
using CodeBlock.DevKit.Contracts.Dtos;
using CodeBlock.DevKit.Core.Helpers;
using HeyItIsMe.Application.Dtos.Facts;
using HeyItIsMe.Application.UseCases.Facts.AddFact;
using HeyItIsMe.Application.UseCases.Facts.RemoveFact;
using HeyItIsMe.Application.UseCases.Facts.UpdateFact;

namespace HeyItIsMe.Application.Services.Facts;

/// <summary>
/// Application service for managing Fact entities.
/// This class demonstrates how to implement application services that coordinate between
/// different use cases and provide a clean API for the presentation layer.
/// </summary>
internal class FactService : ApplicationService, IFactService
{
    /// <summary>
    /// Initializes a new instance of the FactService with the required dependencies.
    /// </summary>
    /// <param name="requestDispatcher">The request dispatcher for sending commands and queries</param>
    public FactService(IRequestDispatcher requestDispatcher)
        : base(requestDispatcher) { }

    /// <summary>
    /// Adds a new fact to a page with the specified data.
    /// This method demonstrates how to delegate to use cases using the request dispatcher.
    /// </summary>
    /// <param name="input">The data for adding the new fact</param>
    /// <returns>A result containing the command execution result or an error</returns>
    public async Task<Result<CommandResult>> AddFact(AddFactDto input)
    {
        return await _requestDispatcher.SendCommand(new AddFactRequest(input.PageId, input.Content));
    }

    /// <summary>
    /// Updates an existing fact with new data.
    /// This method demonstrates how to delegate to use cases using the request dispatcher.
    /// </summary>
    /// <param name="input">The data for updating the fact</param>
    /// <returns>A result containing the command execution result or an error</returns>
    public async Task<Result<CommandResult>> UpdateFact(UpdateFactDto input)
    {
        return await _requestDispatcher.SendCommand(new UpdateFactRequest(input.PageId, input.FactId, input.Content));
    }

    /// <summary>
    /// Removes a fact from a page.
    /// This method demonstrates how to delegate to use cases using the request dispatcher.
    /// </summary>
    /// <param name="pageId">The unique identifier of the page containing the fact</param>
    /// <param name="factId">The unique identifier of the fact to remove</param>
    /// <returns>A result containing the command execution result or an error</returns>
    public async Task<Result<CommandResult>> RemoveFact(string pageId, string factId)
    {
        return await _requestDispatcher.SendCommand(new RemoveFactRequest(pageId, factId));
    }
}
