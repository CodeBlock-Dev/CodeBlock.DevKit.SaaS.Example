using CodeBlock.DevKit.Contracts.Dtos;
using CodeBlock.DevKit.Core.Helpers;

namespace HeyItIsMe.Application.Services.Contacts;

/// <summary>
/// Service interface for managing Contact entities.
/// This interface demonstrates how to define service contracts with proper return types
/// and async operations following CQRS patterns.
/// </summary>
public interface IContactService
{
    /// <summary>
    /// Adds a new contact to a page with the specified data.
    /// </summary>
    /// <param name="input">The data for adding the new contact</param>
    /// <returns>A result containing the command execution result or an error</returns>
    Task<Result<CommandResult>> AddContact(AddContactDto input);

    /// <summary>
    /// Updates an existing contact with new data.
    /// </summary>
    /// <param name="input">The data for updating the contact</param>
    /// <returns>A result containing the command execution result or an error</returns>
    Task<Result<CommandResult>> UpdateContact(UpdateContactDto input);

    /// <summary>
    /// Removes a contact from a page.
    /// </summary>
    /// <param name="pageId">The unique identifier of the page containing the contact</param>
    /// <param name="contactId">The unique identifier of the contact to remove</param>
    /// <returns>A result containing the command execution result or an error</returns>
    Task<Result<CommandResult>> RemoveContact(string pageId, string contactId);
}
