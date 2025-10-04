using CodeBlock.DevKit.Contracts.Dtos;

namespace HeyItIsMe.Application.Dtos.Questions;

public class SearchQuestionsInputDto : SearchInputDto
{
    public SearchQuestionsInputDto()
    {
        RecordsPerPage = 10;
    }
}
