using BuildingBlocks.ApplicationPorts.Messeging;
using UserMgmt.Application.DTOs;
namespace UserMgmt.Application.Quries;

public sealed record GetUsersQuery : IQuery<IReadOnlyList<UserListItem>>;