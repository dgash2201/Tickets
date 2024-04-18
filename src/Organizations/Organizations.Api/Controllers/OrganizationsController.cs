using Microsoft.AspNetCore.Mvc;
using Organizations.Api.Models.Requests;
using Organizations.Application.Dto;
using Organizations.Application.Exceptions;
using Organizations.Application.Interfaces;

namespace Organizations.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrganizationsController : ControllerBase
{
    private readonly IOrganizationService _organizationService;

    public OrganizationsController(IOrganizationService organizationService)
    {
        _organizationService = organizationService;
    }

    [HttpGet("{organizationId:guid}")]
    public async Task<ActionResult<OrganizationDto>> GetOrganization(Guid organizationId)
    {
        var organization = await _organizationService.GetById(organizationId);
        
        return organization is null ? NotFound() : Ok(organization);
    }
    
    [HttpPost]
    [Route("Register")]
    public async Task<ActionResult<Guid>> Register(RegisterOrganizationRequestModel model)
    {
        try
        {
            var userId = await _organizationService.Register(model.Login, model.Name, model.Password, model.Inn,
                model.Ogrn);

            return Ok(userId);
        }
        catch (RegistrationException e)
        {
            return Conflict(e.Message);
        }
    }
}