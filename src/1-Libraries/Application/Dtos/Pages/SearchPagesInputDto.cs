using CodeBlock.DevKit.Contracts.Dtos;

namespace HeyItIsMe.Application.Dtos.Pages;

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
