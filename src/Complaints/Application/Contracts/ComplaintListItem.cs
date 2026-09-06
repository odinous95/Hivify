using Complaints.Domain;

namespace Complaints.Application.Contracts;

public sealed record ComplaintListItem(
    Guid Id,
    string Title,
    string Description,
    Guid AssociationId,
    ComplaintStatus Status,
    DateTime CreatedDate,
    string? ImageUrl,
    string? AdminComment = null,
    string? UserId = null

);