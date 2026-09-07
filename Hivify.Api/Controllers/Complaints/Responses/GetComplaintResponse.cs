namespace Hivify.Api.Controllers.Complaints.Responses;

public sealed record GetComplaintResponse(
    Guid Id,
    Guid AssociationId,
    string Title,
    string Description,
    string Status,
    string? AdminComment,
    string? ImageUrl,
    DateTimeOffset CreatedAt);
