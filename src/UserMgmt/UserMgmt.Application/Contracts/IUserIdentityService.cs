using UserMgmt.Application.DTOs;

namespace UserMgmt.Application.Contracts;


public interface IUserIdentityService
{
    Task<RegisterUserResult> RegisterAsync(
        string email,
        string password,
        string fullName,
        CancellationToken cancellationToken);

    Task<LoginResultDto> LoginAsync(
        string email,
        string password,
        bool rememberMe,
        CancellationToken cancellationToken);

    Task LogoutAsync(
        CancellationToken cancellationToken);
}


