using CodeBlock.DevKit.Contracts.Dtos;

namespace HeyItIsMe.Application.Dtos.Pages;

/// <summary>
/// Data Transfer Object for search input parameters when searching Page entities.
/// This class demonstrates how to create search DTOs that extend base search DTOs and include
/// custom filtering options with sensible defaults.
/// </summary>
public class SearchPagesInputDto : SearchInputDto
{
    /// <summary>
    /// Initializes a new instance with a default page size of 10 records per page.
    /// This demonstrates how to set sensible defaults for search parameters.
    /// </summary>
    public SearchPagesInputDto()
    {
        RecordsPerPage = 10;
    }
}
