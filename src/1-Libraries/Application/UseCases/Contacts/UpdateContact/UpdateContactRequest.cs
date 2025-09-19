using System.ComponentModel.DataAnnotations;
using HeyItIsMe.Core.Resources;
using CodeBlock.DevKit.Application.Commands;
using CodeBlock.DevKit.Core.Resources;

namespace HeyItIsMe.Application.UseCases.Contacts.UpdateContact;

/// <summary>
/// Command request for updating an existing Contact on a Page.
/// This class demonstrates how to implement update command requests with validation attributes,
/// immutable properties, and proper resource-based localization.
/// </summary>
internal class UpdateContactRequest : BaseCommand
{
    /// <summary>
    /// Initializes a new instance of the UpdateContactRequest with the required data.
    /// </summary>
    /// <param name="pageId">The unique identifier of the page containing the contact</param>
    /// <param name="contactId">The unique identifier of the contact to update</param>
    /// <param name="content">The new content for the contact</param>
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
