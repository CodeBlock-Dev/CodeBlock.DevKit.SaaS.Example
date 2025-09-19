using CodeBlock.DevKit.Contracts.Dtos;

namespace HeyItIsMe.Application.Dtos.Facts;

/// <summary>
/// Data Transfer Object for retrieving Fact entity data.
/// This class demonstrates how to create response DTOs that extend base DTOs and include
/// additional properties for display purposes.
/// </summary>
public class GetFactDto : GetEntityDto
{
    /// <summary>
    /// The content of the fact for display purposes.
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    /// The unique identifier of the page that contains this fact.
    /// </summary>
    public string PageId { get; set; }
}
