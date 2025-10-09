using AutoMapper;
using CodeBlock.DevKit.Application.Queries;
using CodeBlock.DevKit.Application.Srvices;
using CodeBlock.DevKit.Core.Extensions;
using HeyItIsMe.Application.Dtos.PageVisits;
using HeyItIsMe.Application.Exceptions;
using HeyItIsMe.Application.Helpers;
using HeyItIsMe.Core.Domain.Pages;
using HeyItIsMe.Core.Domain.Reports;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HeyItIsMe.Application.UseCases.PageVisits.GetPageVisitsByDay;

internal class GetPageVisitsByDayUseCase : BaseQueryHandler, IRequestHandler<GetPageVisitsByDayRequest, IEnumerable<GetPageVisitstByDayDto>>
{
    private readonly IPageRepository _pageRepository;
    private readonly IPageVisitRepository _pageVisitRepository;
    private readonly ICurrentUser _currentUser;

    public GetPageVisitsByDayUseCase(
        IPageRepository pageRepository,
        IPageVisitRepository pageVisitRepository,
        IMapper mapper,
        ILogger<GetPageVisitsByDayUseCase> logger,
        ICurrentUser currentUser
    )
        : base(mapper, logger)
    {
        _pageRepository = pageRepository;
        _pageVisitRepository = pageVisitRepository;
        _currentUser = currentUser;
    }

    public async Task<IEnumerable<GetPageVisitstByDayDto>> Handle(GetPageVisitsByDayRequest request, CancellationToken cancellationToken)
    {
        var page = await _pageRepository.GetByIdAsync(request.PageId);
        if (page == null)
            throw PageApplicationExceptions.PageNotFound(request.PageId);

        EnsureUserHasAccess(page.UserId, _currentUser, Permissions.Page.PAGES);

        var allDates = Enumerable
            .Range(0, (request.ToDateTime - request.FromDateTime).Days + 1)
            .Select(offset => request.FromDateTime.AddDays(offset).Date)
            .ToList();

        var totalVisitsByDay = await _pageVisitRepository.GetVisitsByDay(request.PageId, request.FromDateTime, request.ToDateTime);
        var authorizedVisitsByDay = await _pageVisitRepository.GetAuthorizedVisitsByDay(request.PageId, request.FromDateTime, request.ToDateTime);
        var unauthorizedVisitsByDay = await _pageVisitRepository.GetUnauthorizedVisitsByDay(request.PageId, request.FromDateTime, request.ToDateTime);
        var uniqueVisitorsByDay = await _pageVisitRepository.GetUniqueVisitorsByDay(request.PageId, request.FromDateTime, request.ToDateTime);

        return new List<GetPageVisitstByDayDto>
        {
            new GetPageVisitstByDayDto
            {
                Type = PageVisitType.Total,
                Data = allDates
                    .Select(date => new GetPageVisitstByDayDataPointsDto
                    {
                        Date = date.ToShortDateString(request.FromDateTime, request.ToDateTime),
                        Value = totalVisitsByDay.TryGetValue(date, out var totalCount) ? totalCount : 0,
                    })
                    .ToList(),
            },
            new GetPageVisitstByDayDto
            {
                Type = PageVisitType.Authorized,
                Data = allDates
                    .Select(date => new GetPageVisitstByDayDataPointsDto
                    {
                        Date = date.ToShortDateString(request.FromDateTime, request.ToDateTime),
                        Value = authorizedVisitsByDay.TryGetValue(date, out var authorizedCount) ? authorizedCount : 0,
                    })
                    .ToList(),
            },
            new GetPageVisitstByDayDto
            {
                Type = PageVisitType.Unauthorized,
                Data = allDates
                    .Select(date => new GetPageVisitstByDayDataPointsDto
                    {
                        Date = date.ToShortDateString(request.FromDateTime, request.ToDateTime),
                        Value = unauthorizedVisitsByDay.TryGetValue(date, out var unauthorizedCount) ? unauthorizedCount : 0,
                    })
                    .ToList(),
            },
            new GetPageVisitstByDayDto
            {
                Type = PageVisitType.UniqueVisitors,
                Data = allDates
                    .Select(date => new GetPageVisitstByDayDataPointsDto
                    {
                        Date = date.ToShortDateString(request.FromDateTime, request.ToDateTime),
                        Value = uniqueVisitorsByDay.TryGetValue(date, out var uniqueCount) ? uniqueCount : 0,
                    })
                    .ToList(),
            },
        };
    }
}
