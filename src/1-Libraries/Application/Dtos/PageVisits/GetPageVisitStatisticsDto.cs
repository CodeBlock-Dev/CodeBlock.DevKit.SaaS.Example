namespace HeyItIsMe.Application.Dtos.PageVisits;

public class GetPageVisitStatisticsDto
{
    public long TotalVisits { get; set; }
    public long AuthorizedVisits { get; set; }
    public long UnauthorizedVisits { get; set; }
    public long UniqueVisitors { get; set; }
}
