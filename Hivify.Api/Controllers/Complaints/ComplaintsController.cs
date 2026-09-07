
using BuildingBlocks.ApplicationPorts.Messeging;
using Complaints.Application.Commands.CreateComplaint;
using Complaints.Application.Commands.UpdateComplaintStatus;
using Complaints.Application.Queries.GetComplaint;
using Complaints.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Complaints.Api.Controllers.Complaints;

[ApiController]
[Route("api/complaints")]
public sealed class ComplaintsController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IQuerySender _querySender;

    public ComplaintsController(
        ISender sender,
        IQuerySender querySender)
    {
        _sender = sender;
        _querySender = querySender;
    }

    [HttpGet]
    public async Task<IActionResult> GetComplaints(
        CancellationToken cancellationToken)
    {
        var result = await _querySender.Send(
            new GetAllComplaintsQuery(),
            cancellationToken);

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetComplaint(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _querySender.Send(
            new GetComplaintByIdQuery(id),
            cancellationToken);

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateComplaint(
        CreateComplaintRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateComplaintCommand(
            request.AssociationId,
            request.Title,
            request.Description,
            request.ImageUrl);

        var id = await _sender.Send(
            command,
            cancellationToken);

        return CreatedAtAction(
            nameof(GetComplaint),
            new { id },
            id);
    }

    [HttpPut("{id:guid}/status")]
    public async Task<IActionResult> UpdateComplaintStatus(
        Guid id,
        UpdateComplaintStatusRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateComplaintStatusCommand(
            id,
            request.Status,
            request.AdminComment);

        var result = await _sender.Send(
            command,
            cancellationToken);

        return result
            ? NoContent()
            : NotFound();
    }
}

public sealed record CreateComplaintRequest(
    Guid AssociationId,
    string Title,
    string Description,
    string? ImageUrl);

public sealed record UpdateComplaintStatusRequest(
    ComplaintStatus Status,
    string? AdminComment);

