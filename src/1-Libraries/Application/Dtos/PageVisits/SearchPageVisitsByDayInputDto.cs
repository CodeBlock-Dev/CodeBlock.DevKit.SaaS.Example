namespace HeyItIsMe.Application.Dtos.PageVisits;

public class SearchPageVisitsByDayInputDto
{
    public SearchPageVisitsByDayInputDto()
    {
        var today = DateTime.Today;
        FromDateTime = today.AddDays(-10);
        ToDateTime = today.AddDays(1).AddTicks(-1);
    }

    public string PageId { get; set; }
    public DateTime FromDateTime { get; set; }
    public DateTime ToDateTime { get; set; }
}
