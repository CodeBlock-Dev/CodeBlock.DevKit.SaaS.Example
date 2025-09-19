using AutoMapper;
using CodeBlock.DevKit.Application.Queries;
using CodeBlock.DevKit.Contracts.Dtos;
using CodeBlock.DevKit.Contracts.Services;
using CodeBlock.DevKit.Core.Extensions;
using CodeBlock.DevKit.Core.Helpers;
using HeyItIsMe.Application.Dtos.Pages;
using HeyItIsMe.Core.Domain.Pages;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HeyItIsMe.Application.UseCases.Pages.SearchPages;

internal class SearchPagesUseCase : BaseQueryHandler, IRequestHandler<SearchPagesRequest, SearchOutputDto<GetPageDto>>
{
    private readonly IPageRepository _pageRepository;
    private readonly IUserAccessorService _userAccessorService;

    public SearchPagesUseCase(
        IPageRepository pageRepository,
        IMapper mapper,
        ILogger<SearchPagesUseCase> logger,
        IUserAccessorService userAccessorService
    )
        : base(mapper, logger)
    {
        _pageRepository = pageRepository;
        _userAccessorService = userAccessorService;
    }

    public async Task<SearchOutputDto<GetPageDto>> Handle(SearchPagesRequest request, CancellationToken cancellationToken)
    {
        // Replace the searched user email with user Id if the term is a valid email
        // This allows searching by user email as well as user Id
        var term = await ReplaceSearchedUserEmailWithUserId(request.Term);

        var pages = await _pageRepository.SearchAsync(
            term,
            request.PageNumber,
            request.RecordsPerPage,
            request.SortOrder,
            request.FromDateTime,
            request.ToDateTime
        );

        var totalRecords = await _pageRepository.CountAsync(request.Term, request.FromDateTime, request.ToDateTime);

        var pagesDto = _mapper.Map<IEnumerable<GetPageDto>>(pages);

        // Fetch the email associated with the user Id
        foreach (var pageDto in pagesDto)
            pageDto.UserEmail = await _userAccessorService.GetEmailByUserIdIfExists(pageDto.UserId);

        return new SearchOutputDto<GetPageDto> { TotalRecords = totalRecords, Items = pagesDto };
    }

    private async Task<string> ReplaceSearchedUserEmailWithUserId(string term)
    {
        if (term.IsNullOrEmptyOrWhiteSpace())
            return string.Empty;

        term = term.Trim();

        if (term.IsValidEmail())
        {
            var userId = await _userAccessorService.GetUserIdByEmailIfExists(term);
            if (!userId.IsNullOrEmptyOrWhiteSpace())
                term = userId;
        }

        return term;
    }
}
