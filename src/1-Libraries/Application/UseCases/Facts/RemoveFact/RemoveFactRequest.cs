using CodeBlock.DevKit.Application.Commands;
using HeyItIsMe.Application.Exceptions;
using HeyItIsMe.Core.Domain.Pages;
using CodeBlock.DevKit.Application.Srvices;
using CodeBlock.DevKit.Core.Helpers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HeyItIsMe.Application.UseCases.Facts.RemoveFact;

/// <summary>
/// Command request for removing a Fact from a Page.
/// This class demonstrates how to implement command requests with immutable properties.
/// </summary>
internal class RemoveFactRequest : BaseCommand
{
    /// <summary>
    /// Initializes a new instance of the RemoveFactRequest with the required data.
    /// </summary>
    /// <param name="pageId">The unique identifier of the page containing the fact</param>
    /// <param name="factId">The unique identifier of the fact to remove</param>
    public RemoveFactRequest(string pageId, string factId)
    {
        PageId = pageId;
        FactId = factId;
    }

    /// <summary>
    /// The unique identifier of the page containing the fact.
    /// </summary>
    public string PageId { get; }

    /// <summary>
    /// The unique identifier of the fact to remove.
    /// </summary>
    public string FactId { get; }
}
