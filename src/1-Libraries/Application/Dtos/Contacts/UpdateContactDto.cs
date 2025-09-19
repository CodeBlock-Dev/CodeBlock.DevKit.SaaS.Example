using System.ComponentModel.DataAnnotations;
using CodeBlock.DevKit.Core.Resources;
using HeyItIsMe.Core.Resources;

namespace HeyItIsMe.Application.Dtos.Contacts;

/// <summary>
/// Data Transfer Object for updating an existing Contact on a Page.
/// This class demonstrates how to create update DTOs with validation attributes and resource-based localization.
/// </summary>
public class UpdateContactDto
{
    /// <summary>
    /// The unique identifier of the page containing the contact.
    /// </summary>
    [Required(ErrorMessageResourceName = nameof(CoreResource.Required), ErrorMessageResourceType = typeof(CoreResource))]
    public string PageId { get; set; }

    /// <summary>
    /// The unique identifier of the contact to update.
    /// </summary>
    [Required(ErrorMessageResourceName = nameof(CoreResource.Required), ErrorMessageResourceType = typeof(CoreResource))]
    public string ContactId { get; set; }

    /// <summary>
    /// The new content for the contact. Required field that cannot be empty.
    /// </summary>
    [Display(Name = nameof(SharedResource.Contact_Content), ResourceType = typeof(SharedResource))]
    [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.Contact_Content))]
    public string Content { get; set; }
}
