using AutoMapper;
using CodeBlock.DevKit.Application.Queries;
using CodeBlock.DevKit.Application.Srvices;
using HeyItIsMe.Application.Dtos.PageVisits;
using HeyItIsMe.Application.Exceptions;
using HeyItIsMe.Application.Helpers;
using HeyItIsMe.Core.Domain.Pages;
using HeyItIsMe.Core.Domain.Reports;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HeyItIsMe.Application.UseCases.PageVisits.GetPageVisitStatistics;

internal class GetPageVisitStatisticsUseCase : BaseQueryHandler, IRequestHandler<GetPageVisitStatisticsRequest, PageVisitStatisticsDto>
{
    private readonly IPageRepository _pageRepository;
    private readonly IPageVisitRepository _pageVisitRepository;
    private readonly ICurrentUser _currentUser;

    public GetPageVisitStatisticsUseCase(
        IPageRepository pageRepository,
        IPageVisitRepository pageVisitRepository,
        IMapper mapper,
        ILogger<GetPageVisitStatisticsUseCase> logger,
        ICurrentUser currentUser
    )
        : base(mapper, logger)
    {
        _pageRepository = pageRepository;
        _pageVisitRepository = pageVisitRepository;
        _currentUser = currentUser;
    }

    public async Task<PageVisitStatisticsDto> Handle(GetPageVisitStatisticsRequest request, CancellationToken cancellationToken)
    {
        var page = await _pageRepository.GetByIdAsync(request.PageId);
        if (page == null)
            throw PageApplicationExceptions.PageNotFound(request.PageId);

        EnsureUserHasAccess(page.UserId, _currentUser, Permissions.Page.PAGES);

        var totalVisits = await _pageVisitRepository.CountTotalVisits(request.PageId);
        var authorizedVisits = await _pageVisitRepository.CountAuthorizedVisits(request.PageId);
        var unauthorizedVisits = await _pageVisitRepository.CountUnauthorizedVisits(request.PageId);
        var uniqueVisitors = await _pageVisitRepository.CountUniqueVisitors(request.PageId);

        return new PageVisitStatisticsDto
        {
            TotalVisits = totalVisits,
            AuthorizedVisits = authorizedVisits,
            UnauthorizedVisits = unauthorizedVisits,
            UniqueVisitors = uniqueVisitors,
        };
    }
}
