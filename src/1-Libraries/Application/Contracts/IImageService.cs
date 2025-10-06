namespace HeyItIsMe.Application.Contracts;

public interface IImageService
{
    Task<string> GetBase64FromImageUrl(string imageRelativeUrl);
    Task<string> SaveImageFileAsync(string fileName, string base64Image, params string[] paths);
}
