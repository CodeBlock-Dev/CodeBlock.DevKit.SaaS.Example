namespace HeyItIsMe.Application.Dtos.PageVisits;

public class GetPageVisitDto
{
    public string Id { get; set; }
    public string PageId { get; set; }
    public string PageDisplayName { get; set; }
    public string VisitorId { get; set; }
    public string IP { get; set; }
    public DateTime CreatedAt { get; set; }
}
