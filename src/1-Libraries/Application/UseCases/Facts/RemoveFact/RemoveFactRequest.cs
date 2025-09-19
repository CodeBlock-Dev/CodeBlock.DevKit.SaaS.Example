using CodeBlock.DevKit.Application.Commands;

namespace HeyItIsMe.Application.UseCases.Facts.RemoveFact;

internal class RemoveFactRequest : BaseCommand
{
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
