using FluentValidation;
namespace Hivify.Api.Controllers.Feeds.Requests
{

    public sealed record UpdateFeedRequest(
    string Title,
    string Description);

    public sealed class UpdateFeedRequestValidator : AbstractValidator<UpdateFeedRequest>
    {
        public UpdateFeedRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(100);
            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(500);
        }
    }

}


