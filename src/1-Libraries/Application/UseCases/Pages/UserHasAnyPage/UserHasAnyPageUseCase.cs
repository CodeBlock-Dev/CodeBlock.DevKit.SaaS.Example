using AutoMapper;
using CodeBlock.DevKit.Application.Queries;
using CodeBlock.DevKit.Application.Srvices;
using HeyItIsMe.Application.Helpers;
using HeyItIsMe.Core.Domain.Pages;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HeyItIsMe.Application.UseCases.Pages.UserHasAnyPage;

internal class UserHasAnyPageUseCase : BaseQueryHandler, IRequestHandler<UserHasAnyPageRequest, bool>
{
    private readonly IPageRepository _pageRepository;
    private readonly ICurrentUser _currentUser;

    public UserHasAnyPageUseCase(IPageRepository pageRepository, IMapper mapper, ILogger<UserHasAnyPageUseCase> logger, ICurrentUser currentUser)
        : base(mapper, logger)
    {
        _pageRepository = pageRepository;
        _currentUser = currentUser;
    }

    public async Task<bool> Handle(UserHasAnyPageRequest request, CancellationToken cancellationToken)
    {
        // Ensure the current user can only check their own page existence
        EnsureUserHasAccess(request.UserId, _currentUser, Permissions.Page.PAGES);

        return await _pageRepository.UserHasAnyPageAsync(request.UserId);
    }
}
