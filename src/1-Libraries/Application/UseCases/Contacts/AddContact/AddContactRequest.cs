using System.ComponentModel.DataAnnotations;
using CodeBlock.DevKit.Application.Commands;
using CodeBlock.DevKit.Core.Resources;
using HeyItIsMe.Core.Resources;

namespace HeyItIsMe.Application.UseCases.Contacts.AddContact;

internal class AddContactRequest : BaseCommand
{
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
