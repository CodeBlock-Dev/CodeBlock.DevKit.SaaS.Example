using AutoMapper;
using HeyItIsMe.Application.Dtos.Contacts;
using HeyItIsMe.Application.Dtos.DemoThings;
using HeyItIsMe.Application.Dtos.Facts;
using HeyItIsMe.Application.Dtos.Pages;
using HeyItIsMe.Core.Domain.DemoThings;
using HeyItIsMe.Core.Domain.Pages;

namespace HeyItIsMe.Infrastructure.Mapping;

internal class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<DemoThing, GetDemoThingDto>();

        CreateMap<Page, GetPageDto>();

        CreateMap<Fact, GetFactDto>();

        CreateMap<Contact, GetContactDto>();
    }
}
