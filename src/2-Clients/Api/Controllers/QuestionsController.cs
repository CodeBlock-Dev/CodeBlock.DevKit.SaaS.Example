using CodeBlock.DevKit.Contracts.Dtos;
using CodeBlock.DevKit.Core.Helpers;
using CodeBlock.DevKit.Web.Api.Filters;
using HeyItIsMe.Application.Dtos.Questions;
using HeyItIsMe.Application.Helpers;
using HeyItIsMe.Application.Services.Questions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HeyItIsMe.Api.Controllers;

[Tags("Questions")]
[Route("questions")]
[Authorize(Permissions.Question.QUESTIONS)]
public class QuestionsController : BaseApiController
{
    private readonly IQuestionService _questionService;

    public QuestionsController(IQuestionService questionService)
    {
        _questionService = questionService;
    }

    [HttpGet]
    [Route("{id}")]
    [Authorize]
    public async Task<Result<GetQuestionDto>> Get(string id)
    {
        return await _questionService.GetQuestion(id);
    }

    [HttpPost]
    [Authorize]
    public async Task<Result<CommandResult>> Post([FromBody] CreateQuestionDto input)
    {
        return await _questionService.CreateQuestion(input);
    }

    [Route("{id}")]
    [HttpPut]
    [Authorize]
    public async Task<Result<CommandResult>> Put(string id, [FromBody] UpdateQuestionDto input)
    {
        return await _questionService.UpdateQuestion(id, input);
    }

    [HttpGet]
    [Route("{pageNumber}/{recordsPerPage}/{sortOrder}")]
    public async Task<Result<SearchOutputDto<GetQuestionDto>>> Get(
        int pageNumber,
        int recordsPerPage,
        SortOrder sortOrder,
        [FromQuery] string term = null,
        [FromQuery] DateTime? fromDateTime = null,
        [FromQuery] DateTime? toDateTime = null
    )
    {
        var dto = new SearchQuestionsInputDto
        {
            Term = term,
            PageNumber = pageNumber,
            RecordsPerPage = recordsPerPage,
            FromDateTime = fromDateTime,
            ToDateTime = toDateTime,
            SortOrder = sortOrder,
        };
        return await _questionService.SearchQuestions(dto);
    }

    [HttpGet]
    [Authorize]
    [Route("ordered")]
    public async Task<Result<IEnumerable<GetQuestionDto>>> GetOrdered()
    {
        return await _questionService.GetOrderedQuestions();
    }
}
