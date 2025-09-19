using CodeBlock.DevKit.Application.Commands;

namespace HeyItIsMe.Application.UseCases.Contacts.RemoveContact;

internal class RemoveContactRequest : BaseCommand
{
    public RemoveContactRequest(string pageId, string contactId)
    {
        PageId = pageId;
        ContactId = contactId;
    }

    /// <summary>
    /// The unique identifier of the page containing the contact.
    /// </summary>
    public string PageId { get; }

    /// <summary>
    /// The unique identifier of the contact to remove.
    /// </summary>
    public string ContactId { get; }
}
