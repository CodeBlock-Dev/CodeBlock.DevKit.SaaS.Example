using System.ComponentModel.DataAnnotations;
using HeyItIsMe.Core.Resources;

namespace HeyItIsMe.Application.Dtos.PageVisits;

public class GetPageVisitstByDayDto
{
    public GetPageVisitstByDayDto() { }

    public PageVisitType Type { get; set; }
    public IEnumerable<GetPageVisitstByDayDataPointsDto> Data { get; set; }
}

public class GetPageVisitstByDayDataPointsDto
{
    public string Date { get; set; }
    public decimal Value { get; set; }
}

public enum PageVisitType
{
    [Display(Name = nameof(SharedResource.PageVisitType_Authorized), ResourceType = typeof(SharedResource))]
    Authorized,

    [Display(Name = nameof(SharedResource.PageVisitType_Unauthorized), ResourceType = typeof(SharedResource))]
    Unauthorized,

    [Display(Name = nameof(SharedResource.PageVisitType_Total), ResourceType = typeof(SharedResource))]
    Total,

    [Display(Name = nameof(SharedResource.PageVisitType_UniqueVisitors), ResourceType = typeof(SharedResource))]
    UniqueVisitors,
}
