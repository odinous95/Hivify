
using BuildingBlocks.ApplicationPorts.Messeging;
using Feeds.Application.Commands.CreateFeed;
using Feeds.Application.Commands.UpdateFeed;
using Feeds.Application.Queries.GetFeeds;
using Microsoft.AspNetCore.Mvc;

namespace Hivify.Api.Controllers.Feeds;

[ApiController]
[Route("api/feeds")]
public sealed class FeedsController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IQuerySender _querySender;

    public FeedsController(
        ISender sender,
        IQuerySender querySender)
    {
        _sender = sender;
        _querySender = querySender;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        CancellationToken cancellationToken)
    {
        var result = await _querySender.Send(
            new GetFeedsQuery(),
            cancellationToken);

        return Ok(result);
    }



    [HttpPost]
    public async Task<IActionResult> Create(
        CreateFeedRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateFeedCommand(
            request.Title,
            request.Description);

        var feedId = await _sender.Send(
            command,
            cancellationToken);
        return CreatedAtAction(
            nameof(GetAll),
            new { feedId },
            null);

    }

    [HttpPut("{feedId:guid}")]
    public async Task<IActionResult> Update(
        Guid feedId,
        UpdateFeedRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateFeedCommand(
            feedId,
            request.Title,
            request.Description);

        await _sender.Send(
            command,
            cancellationToken);

        return NoContent();
    }
}

public sealed record CreateFeedRequest(
    string Title,
    string Description);

public sealed record UpdateFeedRequest(
    string Title,
    string Description);

