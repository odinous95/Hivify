namespace Association.Application.DTOs;

public sealed class AssociationListItem
{
    public Guid Id { get; init; }

    public string Name { get; init; } = string.Empty;

    public IReadOnlyList<StaffMemberItem> StaffMembers { get; init; } = [];
}


