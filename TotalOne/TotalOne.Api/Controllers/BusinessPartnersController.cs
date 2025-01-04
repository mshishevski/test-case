using MediatR;

using Microsoft.AspNetCore.Mvc;

using TotalOne.Api.Extensions;
using TotalOne.Api.Requests;
using TotalOne.Application.Commands;
using TotalOne.Application.Queries;


namespace TotalOne.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BusinessPartnersController : ControllerBase
{
    private readonly ISender _sender;
    public BusinessPartnersController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllBusinessPartners([FromQuery] GetAllBusinessPartnersRequest request)
    {
        var result = await _sender.Send(
            new GetAllBusinessPartnersQuery(request.PageIndex, request.PageSize, request.DescendingSortDirection, request.SortBy));

        return result.GetHttpResult();
    }

    [HttpGet("filter")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> FilterBusinessPartners([FromQuery] FilterBusinessPartnersRequest request)
    {
        var result = await _sender.Send(
            new FilterBusinessPartnersQuery(request.BusinessPartnerId, request.LastUpdateStart, request.LastUpdateEnd, request.Name));

        return result.GetHttpResult();
    }

    [HttpGet("{businessPartnerId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetBusinessPartnerWithAttributes([FromRoute] long businessPartnerId, [FromQuery] GetBusinessPartnerWithAttributesRequest request)
    {
        var result = await _sender.Send(
            new GetBusinessPartnerWithAttributesQuery(businessPartnerId));

        return result.GetHttpResult();
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateBusinessPartner(CreateBusinessPartnerRequest request)
    {
        var result = await _sender.Send(new CreateBusinessPartnerCommand(request.Name));

        return result.GetHttpResult();
    }

    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateBusinessPartner(UpdateBusinessPartnerRequest request)
    {
        var result = await _sender.Send(new UpdateBusinessPartnerCommand(request.BusinessPartnerId, request.Name));

        return result.GetHttpResult();
    }

    [HttpDelete("{businessPartnerId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteBusinessPartner([FromRoute] long businessPartnerId)
    {
        var result = await _sender.Send(new DeleteBusinessPartnerCommand(businessPartnerId));

        return result.GetHttpResult();
    }
}