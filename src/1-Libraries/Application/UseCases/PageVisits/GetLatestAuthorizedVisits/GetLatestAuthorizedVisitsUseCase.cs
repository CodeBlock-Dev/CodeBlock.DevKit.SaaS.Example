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

namespace HeyItIsMe.Application.UseCases.PageVisits.GetLatestAuthorizedVisits;

internal class GetLatestAuthorizedVisitsUseCase : BaseQueryHandler, IRequestHandler<GetLatestAuthorizedVisitsRequest, IEnumerable<GetPageVisitDto>>
{
    private readonly IPageRepository _pageRepository;
    private readonly IPageVisitRepository _pageVisitRepository;
    private readonly ICurrentUser _currentUser;

    public GetLatestAuthorizedVisitsUseCase(
        IPageRepository pageRepository,
        IPageVisitRepository pageVisitRepository,
        IMapper mapper,
        ILogger<GetLatestAuthorizedVisitsUseCase> logger,
        ICurrentUser currentUser
    )
        : base(mapper, logger)
    {
        _pageRepository = pageRepository;
        _pageVisitRepository = pageVisitRepository;
        _currentUser = currentUser;
    }

    public async Task<IEnumerable<GetPageVisitDto>> Handle(GetLatestAuthorizedVisitsRequest request, CancellationToken cancellationToken)
    {
        var page = await _pageRepository.GetByIdAsync(request.PageId);
        if (page == null)
            throw PageApplicationExceptions.PageNotFound(request.PageId);

        EnsureUserHasAccess(page.UserId, _currentUser, Permissions.Page.PAGES);

        var pageVisit = await _pageVisitRepository.GetLatestAuthorizedVisits(request.PageId, request.TakeCount);

        if (pageVisit == null)
            return new List<GetPageVisitDto>();

        var result = new List<GetPageVisitDto>
        {
            new GetPageVisitDto
            {
                Id = pageVisit.Id,
                PageId = pageVisit.PageId,
                PageDisplayName = page.DisplayName,
                VisitorId = pageVisit.VisitorId,
                IP = pageVisit.IP,
                CreatedAt = pageVisit.CreationTime.DateTime,
            },
        };

        return result;
    }
}
