using CodeBlock.DevKit.Application.Commands;
using HeyItIsMe.Application.Exceptions;
using HeyItIsMe.Core.Domain.Pages;
using CodeBlock.DevKit.Application.Srvices;
using CodeBlock.DevKit.Core.Helpers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HeyItIsMe.Application.UseCases.Contacts.RemoveContact;

/// <summary>
/// Command request for removing a Contact from a Page.
/// This class demonstrates how to implement command requests with immutable properties.
/// </summary>
internal class RemoveContactRequest : BaseCommand
{
    /// <summary>
    /// Initializes a new instance of the RemoveContactRequest with the required data.
    /// </summary>
    /// <param name="pageId">The unique identifier of the page containing the contact</param>
    /// <param name="contactId">The unique identifier of the contact to remove</param>
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
