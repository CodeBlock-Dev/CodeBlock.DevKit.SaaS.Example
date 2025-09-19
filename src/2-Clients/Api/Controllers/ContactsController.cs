using CodeBlock.DevKit.Core.Helpers;
using CodeBlock.DevKit.Web.Api.Filters;
using HeyItIsMe.Application.Dtos.Contacts;
using HeyItIsMe.Application.Services.Contacts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HeyItIsMe.Api.Controllers;

[Tags("Contacts")]
[Route("contacts")]
[Authorize]
public class ContactsController : BaseApiController
{
    private readonly IContactService _contactService;

    public ContactsController(IContactService contactService)
    {
        _contactService = contactService;
    }

    [HttpPost]
    public async Task<Result<CommandResult>> Post([FromBody] AddContactDto input)
    {
        return await _contactService.AddContact(input);
    }

    [HttpPut]
    public async Task<Result<CommandResult>> Put([FromBody] UpdateContactDto input)
    {
        return await _contactService.UpdateContact(input);
    }

    [HttpDelete]
    public async Task<Result<CommandResult>> Delete([FromQuery] string pageId, [FromQuery] string contactId)
    {
        return await _contactService.RemoveContact(pageId, contactId);
    }
}
