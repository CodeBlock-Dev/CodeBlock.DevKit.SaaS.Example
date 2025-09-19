using CodeBlock.DevKit.Core.Helpers;
using HeyItIsMe.Application.Dtos.Contacts;

namespace HeyItIsMe.Application.Services.Contacts;

public interface IContactService
{
    /// <summary>
    /// Adds a new contact to a page with the specified data.
    /// </summary>
    /// <param name="input">The data for adding the new contact</param>
    Task<Result<CommandResult>> AddContact(AddContactDto input);

    /// <summary>
    /// Updates an existing contact with new data.
    /// </summary>
    /// <param name="input">The data for updating the contact</param>
    Task<Result<CommandResult>> UpdateContact(UpdateContactDto input);

    /// <summary>
    /// Removes a contact from a page.
    /// </summary>
    /// <param name="pageId">The unique identifier of the page containing the contact</param>
    /// <param name="contactId">The unique identifier of the contact to remove</param>
    Task<Result<CommandResult>> RemoveContact(string pageId, string contactId);
}
