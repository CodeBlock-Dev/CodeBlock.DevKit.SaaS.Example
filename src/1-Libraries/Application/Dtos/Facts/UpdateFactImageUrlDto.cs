using System.ComponentModel.DataAnnotations;
using HeyItIsMe.Core.Resources;

namespace HeyItIsMe.Application.Dtos.Facts;

public class UpdateFactImageUrlDto
{
    /// <summary>
    /// The base64 encoded image data. Required field that cannot be empty.
    /// </summary>
    [Display(Name = nameof(SharedResource.Fact_ImageUrl), ResourceType = typeof(SharedResource))]
    [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.Fact_ImageUrl))]
    public string Base64Image { get; set; }

    /// <summary>
    /// The web root path for saving the image file.
    /// </summary>
    public string WebRootPath { get; set; }
}
