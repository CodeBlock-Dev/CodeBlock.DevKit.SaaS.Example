using CodeBlock.DevKit.Contracts.Dtos;
using CodeBlock.DevKit.Core.Helpers;

namespace HeyItIsMe.Application.Services.Facts;

/// <summary>
/// Service interface for managing Fact entities.
/// This interface demonstrates how to define service contracts with proper return types
/// and async operations following CQRS patterns.
/// </summary>
public interface IFactService
{
    /// <summary>
    /// Adds a new fact to a page with the specified data.
    /// </summary>
    /// <param name="input">The data for adding the new fact</param>
    /// <returns>A result containing the command execution result or an error</returns>
    Task<Result<CommandResult>> AddFact(AddFactDto input);

    /// <summary>
    /// Updates an existing fact with new data.
    /// </summary>
    /// <param name="input">The data for updating the fact</param>
    /// <returns>A result containing the command execution result or an error</returns>
    Task<Result<CommandResult>> UpdateFact(UpdateFactDto input);

    /// <summary>
    /// Removes a fact from a page.
    /// </summary>
    /// <param name="pageId">The unique identifier of the page containing the fact</param>
    /// <param name="factId">The unique identifier of the fact to remove</param>
    /// <returns>A result containing the command execution result or an error</returns>
    Task<Result<CommandResult>> RemoveFact(string pageId, string factId);
}
