using CodeBlock.DevKit.Application.Queries;
using CodeBlock.DevKit.Contracts.Dtos;
using CodeBlock.DevKit.Core.Helpers;
using HeyItIsMe.Application.Dtos.Questions;

namespace HeyItIsMe.Application.UseCases.Questions.SearchQuestions;

internal class SearchQuestionsRequest : BaseQuery<SearchOutputDto<GetQuestionDto>>
{
    public SearchQuestionsRequest(
        string term,
        int pageNumber,
        int recordsPerPage,
        SortOrder sortOrder,
        DateTime? fromDateTime,
        DateTime? toDateTime,
        QueryOptions options = null
    )
        : base(options)
    {
        Term = term;
        RecordsPerPage = recordsPerPage;
        PageNumber = pageNumber;
        SortOrder = sortOrder;
        FromDateTime = fromDateTime;
        ToDateTime = toDateTime;
    }

    public SortOrder SortOrder { get; }

    public string Term { get; }

    public int RecordsPerPage { get; }

    public int PageNumber { get; }

    public DateTime? FromDateTime { get; }

    public DateTime? ToDateTime { get; }
}
