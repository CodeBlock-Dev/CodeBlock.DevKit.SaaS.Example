using System.ComponentModel.DataAnnotations;
using CodeBlock.DevKit.Application.Commands;
using HeyItIsMe.Core.Resources;

namespace HeyItIsMe.Application.UseCases.Pages.UpdatePageReferenceImage;

internal class UpdatePageReferenceImageRequest : BaseCommand
{
    public UpdatePageReferenceImageRequest(string id, string base64Image, string webRootPath)
    {
        Id = id;
        Base64Image = base64Image;
        WebRootPath = webRootPath;
    }

    public string Id { get; }

    [Display(Name = nameof(SharedResource.Page_ReferenceImage), ResourceType = typeof(SharedResource))]
    [Required(ErrorMessageResourceName = nameof(SharedResource.Page_ReferenceImage), ErrorMessageResourceType = typeof(SharedResource))]
    public string Base64Image { get; }

    public string WebRootPath { get; }
}
