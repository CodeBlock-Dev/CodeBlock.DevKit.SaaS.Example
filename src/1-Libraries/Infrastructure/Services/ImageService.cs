using HeyItIsMe.Application.Contracts;
using Microsoft.AspNetCore.Hosting;

namespace HeyItIsMe.Infrastructure.Services;

public class ImageService : IImageService
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ImageService(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<string> GetBase64FromImageUrl(string imageRelativeUrl)
    {
        // Remove query string from URL before using as file path
        var cleanUrl = RemoveQueryString(imageRelativeUrl);
        var imageFilePath = Path.Combine(_webHostEnvironment.WebRootPath, cleanUrl.TrimStart('/'));

        if (!File.Exists(imageFilePath))
            throw new FileNotFoundException($"Image file not found: {imageFilePath}");

        var imageBytes = await File.ReadAllBytesAsync(imageFilePath);
        return Convert.ToBase64String(imageBytes);
    }

    public async Task<string> SaveImageFileAsync(string fileName, string base64Image, params string[] paths)
    {
        // Remove query string from fileName if present
        var cleanFileName = RemoveQueryString(fileName);
        
        // Combine webRootPath with the provided paths
        var allPaths = new string[] { _webHostEnvironment.WebRootPath }
            .Concat(paths)
            .ToArray();
        var uploadsFolder = Path.Combine(allPaths);

        Directory.CreateDirectory(uploadsFolder);

        var imageData = Convert.FromBase64String(base64Image);
        var filePath = Path.Combine(uploadsFolder, cleanFileName);

        await File.WriteAllBytesAsync(filePath, imageData);

        // Generate relative path by removing the webRootPath prefix
        var relativePath = Path.GetRelativePath(_webHostEnvironment.WebRootPath, filePath);
        return "/" + relativePath.Replace("\\", "/");
    }

    private static string RemoveQueryString(string url)
    {
        if (string.IsNullOrEmpty(url))
            return url;

        var queryIndex = url.IndexOf('?');
        return queryIndex >= 0 ? url.Substring(0, queryIndex) : url;
    }
}
