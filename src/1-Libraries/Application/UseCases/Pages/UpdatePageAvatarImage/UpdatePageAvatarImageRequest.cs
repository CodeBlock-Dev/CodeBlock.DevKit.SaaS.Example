using System.ComponentModel.DataAnnotations;
using CodeBlock.DevKit.Application.Commands;
using HeyItIsMe.Core.Resources;

namespace HeyItIsMe.Application.UseCases.Pages.UpdatePageAvatarImage;

internal class UpdatePageAvatarImageRequest : BaseCommand
{
    public UpdatePageAvatarImageRequest(string id, string base64Image)
    {
        Id = id;
        Base64Image = base64Image;
    }

    public string Id { get; }

    [Display(Name = nameof(SharedResource.Page_AvatarImage), ResourceType = typeof(SharedResource))]
    [Required(ErrorMessageResourceName = nameof(SharedResource.Page_AvatarImage), ErrorMessageResourceType = typeof(SharedResource))]
    public string Base64Image { get; }
}
