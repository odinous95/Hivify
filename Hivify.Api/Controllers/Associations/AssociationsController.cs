using Association.Application.Commands.AddAssociation;
using Association.Application.Commands.RemoveStaffMember;
using Association.Application.Queries.GetAssociation;
using Association.Application.Queries.GetAssociations;
using BuildingBlocks.ApplicationPorts.Messeging;
using Microsoft.AspNetCore.Mvc;

namespace Hivify.Api.Controllers.Associations;

[ApiController]
[Route("api/associations")]
public sealed class AssociationsController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IQuerySender _querySender;

    public AssociationsController(
        ISender sender,
        IQuerySender querySender)
    {
        _sender = sender;
        _querySender = querySender;
    }

    [HttpGet]
    public async Task<IActionResult> GetAssociations(
        CancellationToken cancellationToken)
    {
        var result = await _querySender.Send(
            new GetAssociationsQuery(),
            cancellationToken);

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetAssociation(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _querySender.Send(
            new GetAssociationQuery(id),
            cancellationToken);

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAssociation(
        AddAssociationCommand command,
        CancellationToken cancellationToken)
    {
        var id = await _sender.Send(
            command,
            cancellationToken);

        return CreatedAtAction(
            nameof(GetAssociation),
            new { id = id.Value },
            id);
    }

    [HttpPost("{associationId:guid}/members")]
    public async Task<IActionResult> AddMember(
        Guid associationId,
        AddStaffMemberCommand command,
        CancellationToken cancellationToken)
    {
        var memberId = await _sender.Send(
            command,
            cancellationToken);

        return Ok(memberId);
    }

    [HttpDelete("{associationId:guid}/members/{memberId:guid}")]
    public async Task<IActionResult> RemoveMember(
        Guid associationId,
        Guid memberId,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new RemoveStaffMemberCommand(
                associationId,
                memberId),
            cancellationToken);

        return result
            ? NoContent()
            : NotFound();
    }
}