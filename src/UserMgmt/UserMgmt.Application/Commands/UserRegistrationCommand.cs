using BuildingBlocks.ApplicationPorts.Messeging;

namespace UserMgmt.Application.Commands;

public sealed record RegisterUserCommand(
    string Email,
    string Password,
    string FullName) : ICommand<Guid>;
