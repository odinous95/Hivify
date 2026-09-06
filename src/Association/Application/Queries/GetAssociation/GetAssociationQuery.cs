using Association.Application.DTOs;
using BuildingBlocks.ApplicationPorts.Messeging;

namespace Association.Application.Queries.GetAssociation;

public sealed record GetAssociationQuery(Guid AssociationId) : IQuery<AssociationListItem>;


