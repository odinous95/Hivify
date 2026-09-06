using BuildingBlocks.ApplicationPorts.Messeging;
using Microsoft.AspNetCore.Mvc;
using UserMgmt.Application.Commands;

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
}

public sealed record RegisterUserRequest(
    string Email,
    string Password,
    string FullName);