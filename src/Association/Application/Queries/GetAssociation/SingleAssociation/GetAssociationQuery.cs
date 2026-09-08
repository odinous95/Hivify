using Association.Application.DTOs;
using BuildingBlocks.ApplicationPorts.Messeging;

namespace Association.Application.Queries.GetAssociation.SingleAssociation;

public sealed record GetAssociationQuery(Guid AssociationId) : IQuery<AssociationListItem>;


