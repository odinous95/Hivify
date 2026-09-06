using Complaints.Domain;

namespace Complaints.Application.DTOs;

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