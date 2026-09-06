namespace UserMgmt.Application.DTOs;

public sealed record LoginResultDto(
    bool Succeeded,
    bool IsLockedOut = false,
    bool RequiresTwoFactor = false);