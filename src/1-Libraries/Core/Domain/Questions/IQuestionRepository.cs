using CodeBlock.DevKit.Core.Helpers;
using CodeBlock.DevKit.Domain.Services;

namespace HeyItIsMe.Core.Domain.Questions;

public interface IQuestionRepository : IBaseAggregateRepository<Question>
{
    Task<long> CountAsync(string term);

    Task<IEnumerable<Question>> SearchAsync(string term, int pageNumber, int recordsPerPage, SortOrder sortOrder);

    Task<IEnumerable<Question>> GetAllAsync();
}
