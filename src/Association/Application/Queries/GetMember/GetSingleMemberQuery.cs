using Association.Application.DTOs;
using BuildingBlocks.ApplicationPorts.Messeging;

namespace Association.Application.Queries.GetMember
{
    public sealed record GetSingleMemberQuery(Guid AssociationId, Guid MemberId) : IQuery<StaffMemberItem>;
}
