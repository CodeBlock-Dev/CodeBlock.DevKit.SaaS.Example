using System.ComponentModel.DataAnnotations;
using HeyItIsMe.Core.Resources;

namespace HeyItIsMe.Application.Dtos.Pages;

public class UpdatePageDisplayNameDto
{
    /// <summary>
    /// The new display name shown to users. Required field that cannot be empty.
    /// </summary>
    [Display(Name = nameof(SharedResource.Page_DisplayName), ResourceType = typeof(SharedResource))]
    [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.Page_DisplayName))]
    public string DisplayName { get; set; }
}
