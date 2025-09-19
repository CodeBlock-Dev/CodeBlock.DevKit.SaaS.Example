using System.ComponentModel.DataAnnotations;
using CodeBlock.DevKit.Core.Resources;
using HeyItIsMe.Core.Resources;

namespace HeyItIsMe.Application.Dtos.Contacts;

public class AddContactDto
{
    /// <summary>
    /// The unique identifier of the page to add the contact to.
    /// </summary>
    [Required(ErrorMessageResourceName = nameof(CoreResource.Required), ErrorMessageResourceType = typeof(CoreResource))]
    public string PageId { get; set; }

    /// <summary>
    /// The content of the contact to add. Required field that cannot be empty.
    /// </summary>
    [Display(Name = nameof(SharedResource.Contact_Content), ResourceType = typeof(SharedResource))]
    [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.Contact_Content))]
    public string Content { get; set; }
}
