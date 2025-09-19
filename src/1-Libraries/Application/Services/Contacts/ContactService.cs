using CodeBlock.DevKit.Application.Srvices;
using CodeBlock.DevKit.Contracts.Dtos;
using CodeBlock.DevKit.Core.Helpers;
using HeyItIsMe.Application.Dtos.Contacts;
using HeyItIsMe.Application.UseCases.Contacts.AddContact;
using HeyItIsMe.Application.UseCases.Contacts.RemoveContact;
using HeyItIsMe.Application.UseCases.Contacts.UpdateContact;

namespace HeyItIsMe.Application.Services.Contacts;

/// <summary>
/// Application service for managing Contact entities.
/// This class demonstrates how to implement application services that coordinate between
/// different use cases and provide a clean API for the presentation layer.
/// </summary>
internal class ContactService : ApplicationService, IContactService
{
    /// <summary>
    /// Initializes a new instance of the ContactService with the required dependencies.
    /// </summary>
    /// <param name="requestDispatcher">The request dispatcher for sending commands and queries</param>
    public ContactService(IRequestDispatcher requestDispatcher)
        : base(requestDispatcher) { }

    /// <summary>
    /// Adds a new contact to a page with the specified data.
    /// This method demonstrates how to delegate to use cases using the request dispatcher.
    /// </summary>
    /// <param name="input">The data for adding the new contact</param>
    /// <returns>A result containing the command execution result or an error</returns>
    public async Task<Result<CommandResult>> AddContact(AddContactDto input)
    {
        return await _requestDispatcher.SendCommand(new AddContactRequest(input.PageId, input.Content));
    }

    /// <summary>
    /// Updates an existing contact with new data.
    /// This method demonstrates how to delegate to use cases using the request dispatcher.
    /// </summary>
    /// <param name="input">The data for updating the contact</param>
    /// <returns>A result containing the command execution result or an error</returns>
    public async Task<Result<CommandResult>> UpdateContact(UpdateContactDto input)
    {
        return await _requestDispatcher.SendCommand(new UpdateContactRequest(input.PageId, input.ContactId, input.Content));
    }

    /// <summary>
    /// Removes a contact from a page.
    /// This method demonstrates how to delegate to use cases using the request dispatcher.
    /// </summary>
    /// <param name="pageId">The unique identifier of the page containing the contact</param>
    /// <param name="contactId">The unique identifier of the contact to remove</param>
    /// <returns>A result containing the command execution result or an error</returns>
    public async Task<Result<CommandResult>> RemoveContact(string pageId, string contactId)
    {
        return await _requestDispatcher.SendCommand(new RemoveContactRequest(pageId, contactId));
    }
}
