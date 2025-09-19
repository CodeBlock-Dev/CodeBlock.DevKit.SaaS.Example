using AutoMapper;
using CodeBlock.DevKit.Application.Queries;
using CodeBlock.DevKit.Application.Srvices;
using CodeBlock.DevKit.Contracts.Services;
using HeyItIsMe.Application.Dtos.Pages;
using HeyItIsMe.Application.Exceptions;
using HeyItIsMe.Application.Helpers;
using HeyItIsMe.Core.Domain.Pages;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HeyItIsMe.Application.UseCases.Pages.GetPage;

internal class GetPageUseCase : BaseQueryHandler, IRequestHandler<GetPageRequest, GetPageDto>
{
    private readonly IPageRepository _pageRepository;
    private readonly ICurrentUser _currentUser;
    private readonly IUserAccessorService _userAccessorService;

    public GetPageUseCase(
        IPageRepository pageRepository,
        IMapper mapper,
        ILogger<GetPageUseCase> logger,
        ICurrentUser currentUser,
        IUserAccessorService userAccessorService
    )
        : base(mapper, logger)
    {
        _pageRepository = pageRepository;
        _currentUser = currentUser;
        _userAccessorService = userAccessorService;
    }

    public async Task<GetPageDto> Handle(GetPageRequest request, CancellationToken cancellationToken)
    {
        var page = await _pageRepository.GetByIdAsync(request.Id);
        if (page == null)
            throw PageApplicationExceptions.PageNotFound(request.Id);

        // Ensures that the current user has permission to access the specified data
        EnsureUserHasAccess(page.UserId, _currentUser, Permissions.Page.PAGES);

        var pageDto = _mapper.Map<GetPageDto>(page);

        // Fetch the email associated with the user Id
        pageDto.UserEmail = await _userAccessorService.GetEmailByUserIdIfExists(page.UserId);

        return pageDto;
    }
}
