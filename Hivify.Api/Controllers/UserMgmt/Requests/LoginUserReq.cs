using FluentValidation;

namespace Hivify.Api.Controllers.UserMgmt.Requests
{
    public sealed record LoginUserReq(
        string Email,
        string Password,
        bool RememberMe
        );

    public sealed class LoginUserReqValidator : AbstractValidator<LoginUserReq>
    {
        public LoginUserReqValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();
            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(6);
        }
    }
}
