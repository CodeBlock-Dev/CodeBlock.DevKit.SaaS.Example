using CodeBlock.DevKit.Contracts.Dtos;
using HeyItIsMe.Application.Dtos.Contacts;
using HeyItIsMe.Application.Dtos.Facts;

namespace HeyItIsMe.Application.Dtos.Pages;

public class GetPageDto : GetEntityDto
{
    /// <summary>
    /// The unique route for accessing this page for display purposes.
    /// </summary>
    public string Route { get; set; }

    /// <summary>
    /// The display name shown to users for display purposes.
    /// </summary>
    public string DisplayName { get; set; }

    /// <summary>
    /// The unique identifier of the user who owns this page.
    /// </summary>
    public string UserId { get; set; }

    /// <summary>
    /// The email address of the user who owns this page.
    /// This is populated by the service layer for display purposes.
    /// </summary>
    public string UserEmail { get; set; }

    /// <summary>
    /// Collection of contacts associated with this page.
    /// </summary>
    public ICollection<GetContactDto> Contacts { get; set; }

    /// <summary>
    /// Collection of facts associated with this page.
    /// </summary>
    public ICollection<GetFactDto> Facts { get; set; }
}
