using CodeBlock.DevKit.Application.Srvices;
using CodeBlock.DevKit.Core.Helpers;
using HeyItIsMe.Application.Dtos.Contacts;
using HeyItIsMe.Application.UseCases.Contacts.AddContact;
using HeyItIsMe.Application.UseCases.Contacts.RemoveContact;
using HeyItIsMe.Application.UseCases.Contacts.UpdateContact;

namespace HeyItIsMe.Application.Services.Contacts;

internal class ContactService : ApplicationService, IContactService
{
    public ContactService(IRequestDispatcher requestDispatcher)
        : base(requestDispatcher) { }

    public async Task<Result<CommandResult>> AddContact(AddContactDto input)
    {
        return await _requestDispatcher.SendCommand(new AddContactRequest(input.PageId, input.Content));
    }

    public async Task<Result<CommandResult>> UpdateContact(UpdateContactDto input)
    {
        return await _requestDispatcher.SendCommand(new UpdateContactRequest(input.PageId, input.ContactId, input.Content));
    }

    public async Task<Result<CommandResult>> RemoveContact(string pageId, string contactId)
    {
        return await _requestDispatcher.SendCommand(new RemoveContactRequest(pageId, contactId));
    }
}
