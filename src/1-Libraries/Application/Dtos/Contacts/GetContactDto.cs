using CodeBlock.DevKit.Contracts.Dtos;

namespace HeyItIsMe.Application.Dtos.Contacts;

public class GetContactDto : GetEntityDto
{
    /// <summary>
    /// The content of the contact for display purposes.
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    /// The unique identifier of the page that contains this contact.
    /// </summary>
    public string PageId { get; set; }
}
