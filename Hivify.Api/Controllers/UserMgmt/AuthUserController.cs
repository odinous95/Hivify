using BuildingBlocks.ApplicationPorts.Messeging;
using Microsoft.AspNetCore.Mvc;
using UserMgmt.Application.Commands.LoginUser;
using UserMgmt.Application.Commands.RegisterUser;

[ApiController]
[Route("api/auth")]
public sealed class AuthController : ControllerBase
{
    private readonly ISender _sender;

    public AuthController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(
        RegisterUserRequest request,
        CancellationToken cancellationToken)
    {
        var userId = await _sender.Send(
            new RegisterUserCommand(
                request.Email,
                request.Password,
                request.FullName),
            cancellationToken);

        return Ok(new
        {
            userId
        });
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login(
       LoginUserCommand command,
       CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            command,
            cancellationToken);

        if (!result.Succeeded)
        {
            return Unauthorized(result);
        }

        return Ok(result);
    }
}

public sealed record RegisterUserRequest(
    string Email,
    string Password,
    string FullName);