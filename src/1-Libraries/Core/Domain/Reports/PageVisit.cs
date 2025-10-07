using CodeBlock.DevKit.Domain.Entities;

namespace HeyItIsMe.Core.Domain.Reports;

public sealed class PageVisit : Entity
{
    public PageVisit(string pageId, string visitorId, string iP)
    {
        PageId = pageId;
        VisitorId = visitorId;
        IP = iP;
    }

    public string PageId { get; private set; }

    /// <summary>
    /// If the visitor is logged in, this is their user Id
    /// </summary>
    public string VisitorId { get; private set; }
    public string IP { get; private set; }

    public static PageVisit Create(string pageId, string visitorId, string iP)
    {
        return new PageVisit(pageId, visitorId, iP);
    }
}
