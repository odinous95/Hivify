namespace Hivify.Api.Controllers.Feeds.Responses;


public sealed record GetFeedRes(
    Guid Id,
    string Title,
    string Content,
    DateTime CreatedDate);