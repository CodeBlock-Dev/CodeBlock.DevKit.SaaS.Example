using CodeBlock.DevKit.Application.Commands;

namespace HeyItIsMe.Application.UseCases.Facts.RemoveFact;

internal class RemoveFactRequest : BaseCommand
{
    public RemoveFactRequest(string factId)
    {
        FactId = factId;
    }

    /// <summary>
    /// The unique identifier of the fact to remove.
    /// </summary>
    public string FactId { get; }
}
