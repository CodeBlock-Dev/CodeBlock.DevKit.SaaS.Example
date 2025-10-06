using System.ComponentModel.DataAnnotations;
using CodeBlock.DevKit.Application.Commands;
using HeyItIsMe.Core.Resources;

namespace HeyItIsMe.Application.UseCases.Facts.UpdateFactImageUrl;

internal class UpdateFactImageUrlRequest : BaseCommand
{
    public UpdateFactImageUrlRequest(string factId, string base64Image, string webRootPath)
    {
        FactId = factId;
        Base64Image = base64Image;
        WebRootPath = webRootPath;
    }

    /// <summary>
    /// The unique identifier of the fact to update.
    /// </summary>
    public string FactId { get; }

    [Display(Name = nameof(SharedResource.Fact_ImageUrl), ResourceType = typeof(SharedResource))]
    [Required(ErrorMessageResourceName = nameof(SharedResource.Fact_ImageUrl), ErrorMessageResourceType = typeof(SharedResource))]
    public string Base64Image { get; }

    public string WebRootPath { get; }
}

