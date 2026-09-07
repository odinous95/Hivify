using BuildingBlocks.ApplicationPorts.Messeging;
using UserMgmt.Application.DTOs;

namespace UserMgmt.Application.Commands.LoginUser;

public sealed record LoginUserCommand(
    string Email,
    string Password,
    bool RememberMe) : ICommand<LoginResultDto>;