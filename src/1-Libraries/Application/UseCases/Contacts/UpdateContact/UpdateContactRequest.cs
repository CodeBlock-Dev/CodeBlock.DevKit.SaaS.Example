using System.ComponentModel.DataAnnotations;
using CodeBlock.DevKit.Application.Commands;
using CodeBlock.DevKit.Core.Resources;
using HeyItIsMe.Core.Resources;

namespace HeyItIsMe.Application.UseCases.Contacts.UpdateContact;

internal class UpdateContactRequest : BaseCommand
{
    public UpdateContactRequest(string pageId, string contactId, string content)
    {
        PageId = pageId;
        ContactId = contactId;
        Content = content;
    }

    /// <summary>
    /// The unique identifier of the page containing the contact.
    /// </summary>
    public string PageId { get; }

    /// <summary>
    /// The unique identifier of the contact to update.
    /// </summary>
    public string ContactId { get; }

    /// <summary>
    /// The new content for the contact. Required field that cannot be empty.
    /// </summary>
    [Display(Name = nameof(SharedResource.Contact_Content), ResourceType = typeof(SharedResource))]
    [Required(ErrorMessageResourceName = nameof(CoreResource.Required), ErrorMessageResourceType = typeof(CoreResource))]
    public string Content { get; }
}
