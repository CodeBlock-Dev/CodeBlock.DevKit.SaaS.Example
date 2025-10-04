using AutoMapper;
using HeyItIsMe.Application.Dtos.Contacts;
using HeyItIsMe.Application.Dtos.Facts;
using HeyItIsMe.Application.Dtos.Pages;
using HeyItIsMe.Application.Dtos.Questions;
using HeyItIsMe.Core.Domain.Pages;
using HeyItIsMe.Core.Domain.Questions;

namespace HeyItIsMe.Infrastructure.Mapping;

internal class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Page, GetPageDto>();

        CreateMap<Fact, GetFactDto>();

        CreateMap<Contact, GetContactDto>();

        CreateMap<Question, GetQuestionDto>();
    }
}
