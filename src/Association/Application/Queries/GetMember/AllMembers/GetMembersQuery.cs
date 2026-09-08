using Association.Application.DTOs;
using BuildingBlocks.ApplicationPorts.Messeging;

namespace Association.Application.Queries.GetMember.AllMembers
{
    public sealed record GetMembersQuery(Guid AssociationId) : IQuery<IReadOnlyList<StaffMemberItem>>;
}
