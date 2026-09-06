using Association.Application.DTOs;
using BuildingBlocks.ApplicationPorts.Messeging;

namespace Association.Application.Queries.GetAssociations;

public sealed record GetAssociationsQuery : IQuery<IReadOnlyList<AssociationListItem>>;



