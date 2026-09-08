using Association.Application.DTOs;
using BuildingBlocks.ApplicationPorts.Messeging;

namespace Association.Application.Queries.GetAssociation.AllAssociations;

public sealed record GetAssociationsQuery : IQuery<IReadOnlyList<AssociationListItem>>;



