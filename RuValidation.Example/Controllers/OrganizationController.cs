using Microsoft.AspNetCore.Mvc;
using RuValidation.Example.Models;

namespace RuValidation.Example.Controllers;

[ApiController]
[Route("org")]
public class OrganizationController : ControllerBase
{
    private IList<Organization> Organizations { get; } =
    [
        new()
        {
            Inn = 7707329152,
            Kpp = 770701001,
            Ogrn = 1047707030513,
        },
        new()
        {
            Inn = 7708234640,
            Kpp = 770801001,
            Ogrn = 1047708023483,
        },
    ];


    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesErrorResponseType(typeof(void))]
    public ActionResult<Organization> Get([InnLegal] ulong inn, [Kpp] ulong kpp)
    {
        Organization? result = Organizations.FirstOrDefault(x => x.Inn == inn && x.Kpp == kpp);
        return result is not null ? Ok(result) : NotFound();
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public ActionResult<Organization> Post(Organization organization)
    {
        Organizations.Add(organization);
        return CreatedAtAction(nameof(Get), new { inn = organization.Inn, kpp = organization.Kpp }, organization);
    }
}