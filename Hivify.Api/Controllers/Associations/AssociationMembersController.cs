using Association.Application.Commands.RemoveStaffMember;
using BuildingBlocks.ApplicationPorts.Messeging;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/associations/{associationId:guid}/members")]
public sealed class AssociationMembersController : ControllerBase
{
    private readonly ISender _sender;

    public AssociationMembersController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
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

    [HttpDelete("{memberId:guid}")]
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

        return result ? NoContent() : NotFound();
    }
}