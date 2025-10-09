using AutoMapper;
using CodeBlock.DevKit.Application.Queries;
using CodeBlock.DevKit.Application.Srvices;
using HeyItIsMe.Application.Dtos.Pages;
using HeyItIsMe.Application.Exceptions;
using HeyItIsMe.Application.Helpers;
using HeyItIsMe.Core.Domain.Pages;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HeyItIsMe.Application.UseCases.Pages.GetPageByUserId;

internal class GetPageByUserIdUseCase : BaseQueryHandler, IRequestHandler<GetPageByUserIdRequest, GetPageDto>
{
    private readonly IPageRepository _pageRepository;
    private readonly ICurrentUser _currentUser;

    public GetPageByUserIdUseCase(IPageRepository pageRepository, IMapper mapper, ILogger<GetPageByUserIdUseCase> logger, ICurrentUser currentUser)
        : base(mapper, logger)
    {
        _pageRepository = pageRepository;
        _currentUser = currentUser;
    }

    public async Task<GetPageDto> Handle(GetPageByUserIdRequest request, CancellationToken cancellationToken)
    {
        var page = await _pageRepository.GetByUserIdAsync(request.Id);
        if (page == null)
            throw PageApplicationExceptions.PageNotFound(request.Id);

        // Ensures that the current user has permission to access the specified data
        EnsureUserHasAccess(page.UserId, _currentUser, Permissions.Page.PAGES);

        var pageDto = _mapper.Map<GetPageDto>(page);

        return pageDto;
    }
}
