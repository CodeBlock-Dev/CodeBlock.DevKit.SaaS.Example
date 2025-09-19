using System.ComponentModel.DataAnnotations;
using HeyItIsMe.Core.Resources;
using CodeBlock.DevKit.Application.Commands;
using CodeBlock.DevKit.Core.Resources;

namespace HeyItIsMe.Application.UseCases.Contacts.AddContact;

/// <summary>
/// Command request for adding a new Contact to a Page.
/// This class demonstrates how to implement command requests with validation attributes,
/// immutable properties, and proper resource-based localization.
/// </summary>
internal class AddContactRequest : BaseCommand
{
    /// <summary>
    /// Initializes a new instance of the AddContactRequest with the required data.
    /// </summary>
    /// <param name="pageId">The unique identifier of the page to add the contact to</param>
    /// <param name="content">The content of the contact to add</param>
    public AddContactRequest(string pageId, string content)
    {
        PageId = pageId;
        Content = content;
    }

    /// <summary>
    /// The unique identifier of the page to add the contact to.
    /// </summary>
    public string PageId { get; }

    /// <summary>
    /// The content of the contact to add. Required field that cannot be empty.
    /// </summary>
    [Display(Name = nameof(SharedResource.Contact_Content), ResourceType = typeof(SharedResource))]
    [Required(ErrorMessageResourceName = nameof(CoreResource.Required), ErrorMessageResourceType = typeof(CoreResource))]
    public string Content { get; }
}
